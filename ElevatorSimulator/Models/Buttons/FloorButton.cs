﻿using System;
using ElevatorApp.Models.Interfaces;
using ElevatorApp.Util;

namespace ElevatorApp.Models
{
    public class FloorButton : Button<int>, IFloorBoundButton, ISubcriber<(ElevatorMasterController, Elevator)>
    {
        public override string Label => this.FloorNum.ToString();

        public int FloorNum { get; }

        public FloorButton(int floorNum, bool active = false) : base($"FloorBtn {floorNum}")
        {
            this.FloorNum = floorNum;
            this._active = active;
        }

        public override void Push()
        {
            this.Pushed(this, this.FloorNum);
        }

        public bool Subscribed { get; private set; }

        public void Subscribe((ElevatorMasterController, Elevator) parent)
        {
            //if (this.Subscribed)
            //    return;

            var (controller, elevator) = parent;
            this.OnPushed += (a, b) =>
            {
                Logger.LogEvent($"Pushed floor button {this.FloorNum}");
                controller.Dispatch(this.FloorNum, elevator.CurrentFloor > this.FloorNum ? Direction.Down : Direction.Up);
            };
            
            elevator.OnArrival += (e, floor) =>
            {
                if (floor == this.FloorNum)
                    this.ActionCompleted(e, floor);
            };

            elevator.PropertyChanged += (e, args) =>
            {
                if (args.PropertyName == nameof(Elevator.CurrentFloor) && this.FloorNum == elevator.CurrentFloor)
                {
                    this.Disabled = true;
                    
                }

            };
            this.Subscribed = true;
        }
    }
}
