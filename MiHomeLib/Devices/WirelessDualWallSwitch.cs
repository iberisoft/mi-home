using System;
using MiHomeLib.Events;
using Newtonsoft.Json.Linq;

namespace MiHomeLib.Devices
{
    public class WirelessDualWallSwitch : MiHomeDevice
    {
        private const string LeftChannel = "channel_0";
        private const string RightChannel = "channel_1";

        public event EventHandler<WallSwitchEventArgs> OnRightClick;
        public event EventHandler<WallSwitchEventArgs> OnLeftClick;

        public event EventHandler<WallSwitchEventArgs> OnRightDoubleClick;
        public event EventHandler<WallSwitchEventArgs> OnLeftDoubleClick;

        public event EventHandler<WallSwitchEventArgs> OnRightLongClick;
        public event EventHandler<WallSwitchEventArgs> OnLeftLongClick;

        public string StatusLeft { get; private set; }
        public string StatusRight { get; private set; }

        public override void ParseData(string command)
        {
            var jObject = JObject.Parse(command);

            if (jObject[LeftChannel] != null)
            {
                StatusLeft = jObject[LeftChannel].Value<string>();
                if (StatusLeft == "click")
                {
                    OnLeftClick?.Invoke(this, new WallSwitchEventArgs(StatusLeft));
                }
                else if (StatusLeft == "double_click")
                {
                    OnLeftDoubleClick?.Invoke(this, new WallSwitchEventArgs(StatusLeft));
                }
                else if (StatusLeft == "long_click")
                {
                    OnLeftLongClick?.Invoke(this, new WallSwitchEventArgs(StatusLeft));
                }

            }

            if (jObject[RightChannel] != null)
            {
                StatusRight = jObject[RightChannel].Value<string>();
                if (StatusRight == "click")
                {
                    OnRightClick?.Invoke(this, new WallSwitchEventArgs(StatusRight));
                }
                else if (StatusRight == "double_click")
                {
                    OnRightDoubleClick?.Invoke(this, new WallSwitchEventArgs(StatusRight));
                }
                else if (StatusRight == "long_click")
                {
                    OnRightLongClick?.Invoke(this, new WallSwitchEventArgs(StatusRight));
                }
            }

        }

        public WirelessDualWallSwitch(string sid) : base(sid, "remote.b286acn01")
        {

        }

        public override string ToString()
        {
            return $"Status Left: {StatusLeft}, Right: {StatusRight}";
        }
    }
}