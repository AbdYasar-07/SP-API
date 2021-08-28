using SP_API.Model;
using SP_API.Response;
using System;
using System.Collections.Generic;

namespace SP_API.SpService
{
    public interface SharingServiceInterface
    {
        ApiResponse gettingConfiguration(List<ProjectSite> projectSite);

        
    }
}
