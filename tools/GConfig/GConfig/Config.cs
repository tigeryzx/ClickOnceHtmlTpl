using System;
using System.Collections.Generic;
using System.Text;

namespace GConfig
{
    public class Config
    {
        public string Title { get; set; }

        public List<App> Apps { get; set; }
    }

    public class App
    {
        public string DeployassemblyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string RunUrl { get; set; }

        public string StepUrl { get; set; }

        public string IconUrl { get; set; }

        public string Remark { get; set; }

        public string PublishDate { get; set; }
    }
}
