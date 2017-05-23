using FileAndFolderDialogs.CommonClasses;
using FileAndFolderDialogs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileAndFolderDialogs.ViewModels
{
    public class VMATFileDialogModel:ViewModelBase
    {
        private FileFolderInfo _selectedTreeViewItem;
        public FileFolderInfo SelectedTreeViewItem
        {
            get
            {
                return _selectedTreeViewItem;
            }

            set
            {
                _selectedTreeViewItem = value;
                if (value != null)
                {
                    if (value.IsDirectory || value.IsDrive)
                    {
                        value.FileFolders = new ObservableCollection<FileFolderInfo>();
                        GetDirectoryData(new DirectoryInfo(value.Path), ref value);
                        GetFileInfo(new DirectoryInfo(value.Path), ref value);
                    }
                }
                OnPropertyChanged("SelectedTreeViewItem");
            }
        }

        private FileFolderInfo _SelectedFileItem;
        public FileFolderInfo SelectedFileItem
        {
            get
            {
                return _SelectedFileItem;
            }

            set
            {
                _SelectedFileItem = value;
                OnPropertyChanged("SelectedFileItem");
            }
        }

        private string _AvoidedFilePaths;
        public string AvoidedFilePaths
        {
            get
            {
                return _AvoidedFilePaths == null || _AvoidedFilePaths == "" ? "," : _AvoidedFilePaths;
            }

            set
            {
                _AvoidedFilePaths = value;
                OnPropertyChanged("AvoidedFilePaths");
            }
        }

        private ObservableCollection<FileFolderInfo> _SelectedFileItemList;
        public ObservableCollection<FileFolderInfo> SelectedFileItemList
        {
            get
            {
                return _SelectedFileItemList;
            }

            set
            {
                _SelectedFileItemList = value;
                OnPropertyChanged("SelectedFileItemList");
            }
        }

        private ObservableCollection<FileFolderInfo> _TempSelectedFileItemList;
        public ObservableCollection<FileFolderInfo> TempSelectedFileItemList
        {
            get
            {
                return _TempSelectedFileItemList;
            }

            set
            {
                _TempSelectedFileItemList = value;
                OnPropertyChanged("TempSelectedFileItemList");
            }
        }

        private List<string> _FilterList;
        public List<string> FilterList
        {
            get
            {
                return _FilterList == null ? new List<string>() : _FilterList;
            }
            set
            {
                _FilterList = value;
                OnPropertyChanged("FilterList");
            }
        }
        private string _SelectedFilter;
        public string SelectedFilter
        {
            get
            {
                return _SelectedFilter == null ? "" : _SelectedFilter;
            }
            set
            {
                _SelectedFilter = value;
                if (SelectedTreeViewItem != null)
                {
                    if (SelectedTreeViewItem.IsDirectory || SelectedTreeViewItem.IsDrive)
                    {
                        FileFolderInfo obj = SelectedTreeViewItem;
                        SelectedTreeViewItem.FileFolders = new ObservableCollection<FileFolderInfo>();
                        GetDirectoryData(new DirectoryInfo(SelectedTreeViewItem.Path), ref obj);
                        GetFileInfo(new DirectoryInfo(SelectedTreeViewItem.Path), ref obj);
                    }
                }
                OnPropertyChanged("SelectedFilter");
            }
        }

        private bool _IsOk;
        public bool IsOk
        {
            get
            {
                return _IsOk;
            }

            set
            {
                _IsOk = value;
                if (value)
                {
                    if (TempSelectedFileItemList.Count > 0)
                    {
                        SelectedFileItemList = TempSelectedFileItemList;
                    }
                    else
                    {
                        SelectedFileItemList = new ObservableCollection<FileFolderInfo>();
                        SelectedFileItemList.Add(SelectedFileItem);
                    }
                    OnPropertyChanged("IsOk");
                }
            }
        }

        private ObservableCollection<FileFolderInfo> _drives;
        public ObservableCollection<FileFolderInfo> Drives
        {
            get
            {
                return _drives;
            }

            set
            {
                _drives = value;
                OnPropertyChanged("Drives");
            }
        }

        private bool _MultiSelect;
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }

            set
            {
                _MultiSelect = value;
                OnPropertyChanged("MultiSelect");
            }
        }

        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }

            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }

        private static VMATFileDialogModel _instance;
        public static VMATFileDialogModel Instance
        {
            get { return _instance ?? (_instance = new VMATFileDialogModel()); }
        }

        public VMATFileDialogModel()
        {
        }

        public void loadTreeData()
        {
            Drives = new ObservableCollection<FileFolderInfo>();
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                if (!AvoidedFilePaths.Split(',').Contains(info.RootDirectory.FullName))
                {
                    if (info.IsReady)
                    {
                        FileFolderInfo harddriveinfo = new FileFolderInfo();
                        harddriveinfo.FileFolders = new ObservableCollection<FileFolderInfo>();
                        GetDirectoryData(info.RootDirectory, ref harddriveinfo);
                        GetFileInfo(info.RootDirectory, ref harddriveinfo);
                        harddriveinfo.IsDrive = true;
                        harddriveinfo.Name = info.Name;
                        harddriveinfo.Path = info.RootDirectory.FullName;
                        Drives.Add(harddriveinfo);
                    }
                }
            }
        }

        public void GetDirectoryData(DirectoryInfo DirectoryInfo, ref FileFolderInfo folder)
        {
            foreach (DirectoryInfo directoryinfo in DirectoryInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly).Where(s => !AvoidedFilePaths.Contains(s.FullName)))
            {
                if ((directoryinfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    folder.FileFolders.Add(new FileFolderInfo
                    {
                        Name = directoryinfo.Name,
                        Path = directoryinfo.FullName,
                        IsDirectory = true,
                        DateModified = directoryinfo.LastWriteTime
                        //Files= GetFileInfo(directoryinfo),
                        //Folders=GetDirectoryData(directoryinfo)
                    });
                }
            }
        }

        public void GetFileInfo(DirectoryInfo DirectoryInfo, ref FileFolderInfo items)
        {
            if (!SelectedFilter.Contains("*.*"))
            {
                foreach (System.IO.FileInfo directoryinfo in DirectoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(s => SelectedFilter.Split('|')[1].Contains(s.Extension.ToLower())).Where(s => !AvoidedFilePaths.Contains(s.FullName)))
                {
                    if ((directoryinfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden && directoryinfo.Extension != "")
                    {
                        items.FileFolders.Add(new FileFolderInfo
                        {
                            Name = directoryinfo.Name,
                            Path = directoryinfo.FullName,
                            DateModified = directoryinfo.LastWriteTime,
                            IsFile = true
                        });
                    }
                }
            }
            else
            {
                foreach (System.IO.FileInfo directoryinfo in DirectoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(s => !AvoidedFilePaths.Contains(s.FullName)))
                {
                    if ((directoryinfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden && directoryinfo.Extension != "")
                    {
                        items.FileFolders.Add(new FileFolderInfo
                        {
                            Name = directoryinfo.Name,
                            Path = directoryinfo.FullName,
                            DateModified = directoryinfo.LastWriteTime,
                            IsFile = true
                        });
                    }
                }
            }
        }
    }
}
