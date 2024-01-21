namespace RustDetector.api.Entities;

public static class EntityExtensions
{
    public static JobDataDto AsDto(this JobData jobData)
    {
        return new JobDataDto(
            jobData.Id,
            jobData.Month,
            jobData.Year,
            jobData.RustCount,
            jobData.GoCount,
            jobData.PythonCount
        );
    }
}