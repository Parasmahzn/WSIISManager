namespace WSIISManager.Services;

public interface IIISServiceExecution
{
    Task StartSitesAsync(List<string> sites);
    Task StopSitesAsync(List<string> sites);
    Task StartApplicationPoolAsync(List<string> applicationPools);
    Task StopApplicationPoolAsync(List<string> applicationPools);
}
