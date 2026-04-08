#nullable enable

using System;
using UnityEngine;

namespace Uft.Staffroll.Samples
{
    public class StaffrollSample : MonoBehaviour
    {
        [SerializeField] TextAsset? _xmlAsset;
        [SerializeField] StaffrollPlayer? _staffrollPlayer;

        bool _browseMode;

        void Start()
        {
            if (this._xmlAsset == null) throw new Exception("XmlAsset is not assigned.");
            if (this._staffrollPlayer == null) throw new Exception("Staffroll is not assigned.");

            this._staffrollPlayer.onCompleted = () =>
            {
                this._staffrollPlayer.Browse(this._xmlAsset.text);
                this._browseMode = true;
            };

            this._staffrollPlayer.Play(this._xmlAsset.text);
        }

        void Update()
        {
            if (this._staffrollPlayer == null) return;

            if (!this._browseMode) return;
            var delta = Input.GetAxis("Vertical") * 1200f * Time.deltaTime;
            if (delta != 0f) this._staffrollPlayer.Scroll(-delta);
        }
    }
}
