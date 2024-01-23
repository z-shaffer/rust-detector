using System.ComponentModel.DataAnnotations;

namespace RustDetector.api;

public record JobDataDto(
    int Id,
    int Month,
    int Year,
    int RustCount,
    int GoCount,
    int PythonCount
);

public record CreateJobDataDto(
    [Required] int Id,
    int Month,
    int Year,
    int RustCount,
    int GoCount,
    int PythonCount
);

public record UpdateJobDataDto(
    [Required] int Id,
    int Month,
    int Year,
    int RustCount,
    int GoCount,
    int PythonCount
);
