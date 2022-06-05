﻿using JeniusApps.Common.Tools;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;

namespace AmbientSounds.Services
{
    public class FocusToastService : IFocusToastService
    {
        private readonly IToastService _toastService;
        private readonly ILocalizer _localizer;

        public FocusToastService(
            IToastService toastService,
            ILocalizer localizer)
        {
            Guard.IsNotNull(toastService, nameof(toastService));
            Guard.IsNotNull(localizer, nameof(localizer));

            _toastService = toastService;
            _localizer = localizer;
        }

        public void ClearToasts()
        {
            _toastService.ClearScheduledToasts();
        }

        public void SendCompletionToast()
        {
            _toastService.SendToast(
                _localizer.GetString("FocusSessionCompleteTitle"),
                _localizer.GetString("FocusSessionCompleteMessage"));
        }

        public void ScheduleToasts(IReadOnlyList<FocusSession> orderedSessions, DateTime start, bool showStartToast)
        {
            _toastService.ClearScheduledToasts();

            if (showStartToast)
            {
                _toastService.SendToast(
                    _localizer.GetString("FocusSessionStartTitle"),
                    _localizer.GetString("FocusSessionStartMessage"));
            }

            var current = start;
            foreach (var session in orderedSessions)        
            {
                current = current.AddTicks(session.Remaining.Ticks);

                if (session.QueuePosition == session.QueueSize - 1)
                {
                    // last session, so no need to schedule a toast.
                    continue;
                }
                else
                {
                    if (session.SessionType == SessionType.Focus)
                    {
                        _toastService.ScheduleToast(
                            current,
                            _localizer.GetString("RestText"),
                            _localizer.GetString("FocusSessionRestMessage"));
                    }
                    else if (session.SessionType == SessionType.Rest)
                    {
                        _toastService.ScheduleToast(
                            current,
                            _localizer.GetString("FocusText"),
                            _localizer.GetString("FocusSessionFocusMessage"));
                    }
                }
            }
        }
    }
}
