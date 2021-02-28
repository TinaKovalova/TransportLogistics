﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TransportLogistics.Infrastructure;

namespace TransportLogistics.ViewModels
{
    public class MainViewModel:BaseNotifyPropertyChanged
    {
        private UserControl currentView;
        public UserControl CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                Notify();
            }
        }
    }
}