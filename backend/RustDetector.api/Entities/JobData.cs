using System.ComponentModel.DataAnnotations;

namespace RustDetector.api.Entities;

public class JobData
{
    [Required]
    public int Id { get; set; }
    
    public required int Month { get; set; }
    
    public required int Year { get; set; }
    
    public int RustCount { get; set; }
    public int GoCount { get; set; }
    public int PythonCount { get; set; }
    // public int cppCount { get; set; }
}