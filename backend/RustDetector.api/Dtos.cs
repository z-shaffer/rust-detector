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
    [Required][Range(1, 12, ErrorMessage = "Please enter a valid integer within the range of 1 to 12.")] int Month,
    [Required][Range(2023, 3000, ErrorMessage = "Please enter a valid integer within the range of 2023 to 3000.")] int Year,
    int RustCount,
    int GoCount,
    int PythonCount
);

public record UpdateJobDataDto(
    [Required] int Id,
    [Required][Range(1, 12, ErrorMessage = "Please enter a valid integer within the range of 1 to 12.")] int Month,
    [Required][Range(2023, 3000, ErrorMessage = "Please enter a valid integer within the range of 2023 to 3000.")] int Year,
    int RustCount,
    int GoCount,
    int PythonCount
);
