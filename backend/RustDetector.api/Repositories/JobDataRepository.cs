using RustDetector.api.Entities;

namespace RustDetector.api.Repositories;

public class JobDataRepository
{
    private readonly List<JobData> _jobDataList = new()
    {
        new JobData()
        {
            Id = 1,
            Month = 2,
            Year = 2024,
            RustCount = 1000,
            GoCount = 2000,
            PythonCount = 3000
        },
        new JobData()
        {
            Id = 2,
            Month = 3,
            Year = 2025,
            RustCount = 100,
            GoCount = 200,
            PythonCount = 300
        }
    };

    public IEnumerable<JobData> GetAll()
    {
        return _jobDataList;
    }

    public JobData? Get(int id)
    {
        return _jobDataList.Find(jobData => jobData.Id == id);
    }

    public void Create(JobData jobData)
    {
        jobData.Id = _jobDataList.Max(data => data.Id) + 1;
        _jobDataList.Add(jobData);
    }

    public void Update(JobData updatedJobData)
    {
        var index = _jobDataList.FindIndex(jobData => jobData.Id == updatedJobData.Id);
        _jobDataList[index] = updatedJobData;
    }

    public void Delete(int id)
    {
        var index = _jobDataList.FindIndex(jobData => jobData.Id == id);
        _jobDataList.RemoveAt(index);
    }
}