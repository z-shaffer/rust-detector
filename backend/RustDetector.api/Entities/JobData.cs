using System.ComponentModel.DataAnnotations;

namespace RustDetector.api.Entities;

public class JobData
{
    public int Id { get; set; }
    
    [Required]
    [Range(1, 12, ErrorMessage = "Please enter a valid integer within the range of 1 to 12.")]
    public required int Month { get; set; }
    
    [Required] 
    [Range(2023, 3000, ErrorMessage = "Please enter a valid integer within the range of 2023 to 3000.")]
    public required int Year { get; set; }
    
    public int RustCount { get; set; }
    public int GoCount { get; set; }
    public int PythonCount { get; set; }
    // public int cppCount { get; set; }
}