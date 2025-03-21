namespace Persistense.EFCore.DTOs.DTOs;

public record KeyValueRecord
(
    string Key,
    Guid OwnerId,
    string Value
);