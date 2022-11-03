using Microsoft.Web.Administration;

namespace WSIISManager.Services;

public class IISServiceExecution : IIISServiceExecution
{
    ServerManager _server = new();
    public Task StartApplicationPoolAsync(List<string> applicationPools)
    {
        var applicationPoolsLists = GetApplicationPools(applicationPools);

        if (applicationPoolsLists != null && applicationPoolsLists.Count > 0)
        {
            foreach (var item in applicationPoolsLists)
            {
                if (item.State == ObjectState.Stopped)
                {
                    item.Start();
                }
            }
        }
        return Task.CompletedTask;
    }

    public Task StartSitesAsync(List<string> sites)
    {
        var sitesLists = GetSites(sites);
        if (sitesLists != null && sitesLists.Count > 0)
        {
            foreach (var item in sitesLists)
            {
                if (item.State == ObjectState.Stopped)
                {
                    item.Start();
                }
            }
        }
        return Task.CompletedTask;
    }

    public Task StopApplicationPoolAsync(List<string> applicationPools)
    {
        var applicationPoolsLists = GetApplicationPools(applicationPools);
        if (applicationPoolsLists != null && applicationPoolsLists.Count > 0)
        {
            foreach (var item in applicationPoolsLists)
            {
                if (item.State == ObjectState.Started)
                {
                    item.Stop();
                }
            }
        }
        return Task.CompletedTask;
    }

    public Task StopSitesAsync(List<string> sites)
    {
        var sitesLists = GetSites(sites);
        if (sitesLists != null && sitesLists.Count > 0)
        {
            foreach (var item in sitesLists)
            {
                if (item.State == ObjectState.Started)
                {
                    item.Stop();
                }
            }
        }
        return Task.CompletedTask;
    }

    private List<Site> GetSites(List<string> sites)
    {
        return _server.Sites.Where(x => sites.Contains(x.Name)).ToList();

    }
    private List<ApplicationPool> GetApplicationPools(List<string> appPools)
    {
        return _server.ApplicationPools.Where(x => appPools.Contains(x.Name)).ToList();
    }
}

