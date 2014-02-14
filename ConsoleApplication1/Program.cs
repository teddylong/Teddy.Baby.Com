using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri path = new Uri("http://192.168.83.70:8080/tfs");
            TfsConfigurationServer tfs = new TfsConfigurationServer(path);

            ReadOnlyCollection<CatalogNode> collectionNodes = tfs.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);

            foreach (CatalogNode collectionNode in collectionNodes)
            {
                // Use the InstanceId property to get the team project collection
                Guid collectionId = new Guid(collectionNode.Resource.Properties["InstanceId"]);
                TfsTeamProjectCollection teamProjectCollection = tfs.GetTeamProjectCollection(collectionId);

                // Print the name of the team project collection
                Console.WriteLine("Collection: " + teamProjectCollection.Name);

                // Get a catalog of team projects for the collection
                ReadOnlyCollection<CatalogNode> projectNodes = collectionNode.QueryChildren(
                    new[] { CatalogResourceTypes.TeamProject },
                    false, CatalogQueryOptions.None);

                // List the team projects in the collection
                foreach (CatalogNode projectNode in projectNodes)
                {
                    Console.WriteLine(" Team Project: " + projectNode.Resource.DisplayName);
                }
            }





            Console.ReadLine();

        }

        //public bool getLatest(string[] items)
        //{
        //    try
        //    {
        //        Workspace myWorkspace = createWorkspace();

        //        var results = myWorkspace.Get(items, VersionSpec.Latest, RecursionType.Full, GetOptions.Overwrite);
        //        var failures = results.GetFailures();

        //        if (failures.Count > 0)
        //        {
        //            foreach (var fail in failures)
        //            {
                        
        //            }

        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
                
        //        return false;
        //    }
        //}
    }
}
