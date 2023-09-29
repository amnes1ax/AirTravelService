using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirTravelService.DataAccess.PostgresSql.Extensions;

public static class UtcDateExtensions
{
    private const string IsUtcAnnotation = "IsUtc";

    private static readonly ValueConverter<DateTime, DateTime> UtcDateTimeConverter =
        new(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    private static readonly ValueConverter<DateTime?, DateTime?> UtcNullableDateTimeConverter =
        new(v => v, v => v == null ? v : DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

    private static readonly ValueConverter<DateTimeOffset, DateTimeOffset> UtcDateTimeOffsetConverter =
        new(v => v.UtcDateTime, v => v);

    private static readonly ValueConverter<DateTimeOffset?, DateTimeOffset?> UtcNullableDateTimeOffsetConverter =
        new(v => v.HasValue ? v.Value.UtcDateTime : null, v => v);

    private static PropertyBuilder<TProperty> IsUtc<TProperty>(this PropertyBuilder<TProperty> builder,
        bool isUtc = true) => builder.HasAnnotation(IsUtcAnnotation, isUtc);

    private static bool IsUtc(this IReadOnlyAnnotatable property) =>
        (bool?) property.FindAnnotation(IsUtcAnnotation)?.Value ?? true;

    public static void ApplyUtcDateTimeConverter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.IsUtc() is false)
                    continue;

                if (property.ClrType == typeof(DateTime)) property.SetValueConverter(UtcDateTimeConverter);

                if (property.ClrType == typeof(DateTime?)) property.SetValueConverter(UtcNullableDateTimeConverter);

                if (property.ClrType == typeof(DateTimeOffset)) property.SetValueConverter(UtcDateTimeOffsetConverter);

                if (property.ClrType == typeof(DateTimeOffset?))
                    property.SetValueConverter(UtcNullableDateTimeOffsetConverter);
            }
        }
    }
}