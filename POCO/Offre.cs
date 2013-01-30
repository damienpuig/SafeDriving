//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace POCO
{
    public partial class Offre
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual int ClientId
        {
            get { return _clientId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_clientId != value)
                    {
                        if (Client != null && Client.Id != value)
                        {
                            Client = null;
                        }
                        _clientId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _clientId;
    
        public virtual Nullable<int> OffresReferencesId
        {
            get { return _offresReferencesId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_offresReferencesId != value)
                    {
                        if (OffresReference != null && OffresReference.Id != value)
                        {
                            OffresReference = null;
                        }
                        _offresReferencesId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _offresReferencesId;

        #endregion
        #region Navigation Properties
    
        public virtual Client Client
        {
            get { return _client; }
            set
            {
                if (!ReferenceEquals(_client, value))
                {
                    var previousValue = _client;
                    _client = value;
                    FixupClient(previousValue);
                }
            }
        }
        private Client _client;
    
        public virtual OffresReference OffresReference
        {
            get { return _offresReference; }
            set
            {
                if (!ReferenceEquals(_offresReference, value))
                {
                    var previousValue = _offresReference;
                    _offresReference = value;
                    FixupOffresReference(previousValue);
                }
            }
        }
        private OffresReference _offresReference;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupClient(Client previousValue)
        {
            if (previousValue != null && previousValue.Offres.Contains(this))
            {
                previousValue.Offres.Remove(this);
            }
    
            if (Client != null)
            {
                if (!Client.Offres.Contains(this))
                {
                    Client.Offres.Add(this);
                }
                if (ClientId != Client.Id)
                {
                    ClientId = Client.Id;
                }
            }
        }
    
        private void FixupOffresReference(OffresReference previousValue)
        {
            if (previousValue != null && previousValue.Offres.Contains(this))
            {
                previousValue.Offres.Remove(this);
            }
    
            if (OffresReference != null)
            {
                if (!OffresReference.Offres.Contains(this))
                {
                    OffresReference.Offres.Add(this);
                }
                if (OffresReferencesId != OffresReference.Id)
                {
                    OffresReferencesId = OffresReference.Id;
                }
            }
            else if (!_settingFK)
            {
                OffresReferencesId = null;
            }
        }

        #endregion
    }
}
