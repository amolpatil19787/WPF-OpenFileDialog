using FileAndFolderDialogs.CommonClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndFolderDialogs.Models
{
    public class FileFolderInfo:ViewModelBase
    {
        private string _Path;
        public string Path
        {
            get
            {
                return _Path;
            }

            set
            {
                _Path = value;
                OnPropertyChanged("Path");
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _IsFile;
        public bool IsFile
        {
            get
            {
                return _IsFile;
            }

            set
            {
                _IsFile = value;
                OnPropertyChanged("IsFile");
            }
        }

        private bool _IsDirectory;
        public bool IsDirectory
        {
            get
            {
                return _IsDirectory;
            }

            set
            {
                _IsDirectory = value;
                OnPropertyChanged("IsDirectory");
            }
        }

        private bool _IsDrive;
        public bool IsDrive
        {
            get
            {
                return _IsDrive;
            }

            set
            {
                _IsDrive = value;
                OnPropertyChanged("IsDrive");
            }
        }

        private DateTime _DateModified;
        public DateTime DateModified
        {
            get
            {
                return _DateModified;
            }

            set
            {
                _DateModified = value;
                OnPropertyChanged("DateModified");
            }
        }

        private ObservableCollection<FileFolderInfo> _FileFolders;
        public ObservableCollection<FileFolderInfo> FileFolders
        {
            get
            {
                return _FileFolders;
            }

            set
            {
                _FileFolders = value;
                OnPropertyChanged("FileFolders");
            }
        }
    }

    public class HardDriveInfo : ViewModelBase
    {
        private string _Path;
        public string Path
        {
            get
            {
                return _Path;
            }

            set
            {
                _Path = value;
                OnPropertyChanged("Path");
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _IsFile;
        public bool IsFile
        {
            get
            {
                return _IsFile;
            }

            set
            {
                _IsFile = value;
                OnPropertyChanged("IsFile");
            }
        }

        private bool _IsDirectory;
        public bool IsDirectory
        {
            get
            {
                return _IsDirectory;
            }

            set
            {
                _IsDirectory = value;
                OnPropertyChanged("IsDirectory");
            }
        }

        private bool _IsDrive;
        public bool IsDrive
        {
            get
            {
                return _IsDrive;
            }

            set
            {
                _IsDrive = value;
                OnPropertyChanged("IsDrive");
            }
        }

        private ObservableCollection<FileFolderInfo> _FileFolders;
        public ObservableCollection<FileFolderInfo> FileFolders
        {
            get
            {
                return _FileFolders;
            }

            set
            {
                _FileFolders = value;
                OnPropertyChanged("FileFolders");
            }
        }
    }
}
