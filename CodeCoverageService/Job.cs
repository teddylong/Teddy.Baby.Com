using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCoverageService
{
    public class Job
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string startTime;

        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private string endTime;

        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string alive;

        public string Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        private string machine;

        public string Machine
        {
            get { return machine; }
            set { machine = value; }
        }
    }
}
