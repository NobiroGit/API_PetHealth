using System.ComponentModel.DataAnnotations;

namespace PetHealth.Application.Common.DTOs.MedicalDocumentDto;

public class InsertMedicalDocumentDto
{
    [Required] public int AppointmentId { get; init; }

    [Required] [MaxLength(100)] public string DocumentType { get; init; }

    [Required] [MaxLength(500)] public string FileUrl { get; init; }

    [Required] public DateTimeOffset DocumentDate { get; init; }
    [MaxLength(500)] public string? Description { get; init; }
}