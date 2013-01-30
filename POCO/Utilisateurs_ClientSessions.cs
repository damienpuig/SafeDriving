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
    public partial class Utilisateurs_ClientSessions
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual int OffreId
        {
            get;
            set;
        }
    
        public virtual int SessionId
        {
            get { return _sessionId; }
            set
            {
                if (_sessionId != value)
                {
                    if (Session != null && Session.Id != value)
                    {
                        Session = null;
                    }
                    _sessionId = value;
                }
            }
        }
        private int _sessionId;
    
        public virtual Nullable<float> Note
        {
            get;
            set;
        }
    
        public virtual int Utilisateurs_ClientId
        {
            get { return _utilisateurs_ClientId; }
            set
            {
                if (_utilisateurs_ClientId != value)
                {
                    if (Utilisateurs_Client != null && Utilisateurs_Client.Id != value)
                    {
                        Utilisateurs_Client = null;
                    }
                    _utilisateurs_ClientId = value;
                }
            }
        }
        private int _utilisateurs_ClientId;

        #endregion
        #region Navigation Properties
    
        public virtual Session Session
        {
            get { return _session; }
            set
            {
                if (!ReferenceEquals(_session, value))
                {
                    var previousValue = _session;
                    _session = value;
                    FixupSession(previousValue);
                }
            }
        }
        private Session _session;
    
        public virtual Client Utilisateurs_Client
        {
            get { return _utilisateurs_Client; }
            set
            {
                if (!ReferenceEquals(_utilisateurs_Client, value))
                {
                    var previousValue = _utilisateurs_Client;
                    _utilisateurs_Client = value;
                    FixupUtilisateurs_Client(previousValue);
                }
            }
        }
        private Client _utilisateurs_Client;

        #endregion
        #region Association Fixup
    
        private void FixupSession(Session previousValue)
        {
            if (previousValue != null && previousValue.Utilisateurs_ClientSessions.Contains(this))
            {
                previousValue.Utilisateurs_ClientSessions.Remove(this);
            }
    
            if (Session != null)
            {
                if (!Session.Utilisateurs_ClientSessions.Contains(this))
                {
                    Session.Utilisateurs_ClientSessions.Add(this);
                }
                if (SessionId != Session.Id)
                {
                    SessionId = Session.Id;
                }
            }
        }
    
        private void FixupUtilisateurs_Client(Client previousValue)
        {
            if (previousValue != null && previousValue.Utilisateurs_ClientSessions.Contains(this))
            {
                previousValue.Utilisateurs_ClientSessions.Remove(this);
            }
    
            if (Utilisateurs_Client != null)
            {
                if (!Utilisateurs_Client.Utilisateurs_ClientSessions.Contains(this))
                {
                    Utilisateurs_Client.Utilisateurs_ClientSessions.Add(this);
                }
                if (Utilisateurs_ClientId != Utilisateurs_Client.Id)
                {
                    Utilisateurs_ClientId = Utilisateurs_Client.Id;
                }
            }
        }

        #endregion
    }
}