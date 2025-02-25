﻿using DATN_BeamRebar.Commands;
using Nice3point.Revit.Toolkit.External;

namespace DATN_BeamRebar
{
    /// <summary>
    ///     Application entry point
    /// </summary>
    [UsedImplicitly]
    public class Application : ExternalApplication
    {
        public override void OnStartup()
        {
            CreateRibbon();
        }

        private void CreateRibbon()
        {
            var panel = Application.CreatePanel("Commands", "DATN_BeamRebar");

            panel.AddPushButton<StartupCommand>("Execute")
                .SetImage("/DATN_BeamRebar;component/Resources/Icons/RibbonIcon16.png")
                .SetLargeImage("/DATN_BeamRebar;component/Resources/Icons/RibbonIcon32.png");
        }
    }
}