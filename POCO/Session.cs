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
    public partial class Session
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual int TypeSessionId
        {
            get { return _typeSessionId; }
            set
            {
                if (_typeSessionId != value)
                {
                    if (TypeSession != null && TypeSession.Id != value)
                    {
                        TypeSession = null;
                    }
                    _typeSessionId = value;
                }
            }
        }
        private int _typeSessionId;
    
        public virtual int EmployeId
        {
            get { return _employeId; }
            set
            {
                if (_employeId != value)
                {
                    if (Employe != null && Employe.Id != value)
                    {
                        Employe = null;
                    }
                    _employeId = value;
                }
            }
        }
        private int _employeId;
    
        public virtual string Nom
        {
            get;
            set;
        }
    
        public virtual int CircuitId
        {
            get { return _circuitId; }
            set
            {
                if (_circuitId != value)
                {
                    if (Circuit != null && Circuit.Id != value)
                    {
                        Circuit = null;
                    }
                    _circuitId = value;
                }
            }
        }
        private int _circuitId;
    
        public virtual int Date_Id
        {
            get;
            set;
        }
    
        public virtual int EtatSessionsId
        {
            get { return _etatSessionsId; }
            set
            {
                if (_etatSessionsId != value)
                {
                    if (EtatSession != null && EtatSession.Id != value)
                    {
                        EtatSession = null;
                    }
                    _etatSessionsId = value;
                }
            }
        }
        private int _etatSessionsId;
    
        public virtual int NbParticipant
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> HeureDebut
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> HeureFin
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual Circuit Circuit
        {
            get { return _circuit; }
            set
            {
                if (!ReferenceEquals(_circuit, value))
                {
                    var previousValue = _circuit;
                    _circuit = value;
                    FixupCircuit(previousValue);
                }
            }
        }
        private Circuit _circuit;
    
        public virtual Employe Employe
        {
            get { return _employe; }
            set
            {
                if (!ReferenceEquals(_employe, value))
                {
                    var previousValue = _employe;
                    _employe = value;
                    FixupEmploye(previousValue);
                }
            }
        }
        private Employe _employe;
    
        public virtual ICollection<Vehicule> Vehicules
        {
            get
            {
                if (_vehicules == null)
                {
                    var newCollection = new FixupCollection<Vehicule>();
                    newCollection.CollectionChanged += FixupVehicules;
                    _vehicules = newCollection;
                }
                return _vehicules;
            }
            set
            {
                if (!ReferenceEquals(_vehicules, value))
                {
                    var previousValue = _vehicules as FixupCollection<Vehicule>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupVehicules;
                    }
                    _vehicules = value;
                    var newValue = value as FixupCollection<Vehicule>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupVehicules;
                    }
                }
            }
        }
        private ICollection<Vehicule> _vehicules;
    
        public virtual TypeSession TypeSession
        {
            get { return _typeSession; }
            set
            {
                if (!ReferenceEquals(_typeSession, value))
                {
                    var previousValue = _typeSession;
                    _typeSession = value;
                    FixupTypeSession(previousValue);
                }
            }
        }
        private TypeSession _typeSession;
    
        public virtual EtatSession EtatSession
        {
            get { return _etatSession; }
            set
            {
                if (!ReferenceEquals(_etatSession, value))
                {
                    var previousValue = _etatSession;
                    _etatSession = value;
                    FixupEtatSession(previousValue);
                }
            }
        }
        private EtatSession _etatSession;
    
        public virtual ICollection<Utilisateurs_ClientSessions> Utilisateurs_ClientSessions
        {
            get
            {
                if (_utilisateurs_ClientSessions == null)
                {
                    var newCollection = new FixupCollection<Utilisateurs_ClientSessions>();
                    newCollection.CollectionChanged += FixupUtilisateurs_ClientSessions;
                    _utilisateurs_ClientSessions = newCollection;
                }
                return _utilisateurs_ClientSessions;
            }
            set
            {
                if (!ReferenceEquals(_utilisateurs_ClientSessions, value))
                {
                    var previousValue = _utilisateurs_ClientSessions as FixupCollection<Utilisateurs_ClientSessions>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUtilisateurs_ClientSessions;
                    }
                    _utilisateurs_ClientSessions = value;
                    var newValue = value as FixupCollection<Utilisateurs_ClientSessions>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUtilisateurs_ClientSessions;
                    }
                }
            }
        }
        private ICollection<Utilisateurs_ClientSessions> _utilisateurs_ClientSessions;

        #endregion
        #region Association Fixup
    
        private void FixupCircuit(Circuit previousValue)
        {
            if (previousValue != null && previousValue.Sessions.Contains(this))
            {
                previousValue.Sessions.Remove(this);
            }
    
            if (Circuit != null)
            {
                if (!Circuit.Sessions.Contains(this))
                {
                    Circuit.Sessions.Add(this);
                }
                if (CircuitId != Circuit.Id)
                {
                    CircuitId = Circuit.Id;
                }
            }
        }
    
        private void FixupEmploye(Employe previousValue)
        {
            if (previousValue != null && previousValue.Sessions.Contains(this))
            {
                previousValue.Sessions.Remove(this);
            }
    
            if (Employe != null)
            {
                if (!Employe.Sessions.Contains(this))
                {
                    Employe.Sessions.Add(this);
                }
                if (EmployeId != Employe.Id)
                {
                    EmployeId = Employe.Id;
                }
            }
        }
    
        private void FixupTypeSession(TypeSession previousValue)
        {
            if (previousValue != null && previousValue.Sessions.Contains(this))
            {
                previousValue.Sessions.Remove(this);
            }
    
            if (TypeSession != null)
            {
                if (!TypeSession.Sessions.Contains(this))
                {
                    TypeSession.Sessions.Add(this);
                }
                if (TypeSessionId != TypeSession.Id)
                {
                    TypeSessionId = TypeSession.Id;
                }
            }
        }
    
        private void FixupEtatSession(EtatSession previousValue)
        {
            if (previousValue != null && previousValue.Sessions.Contains(this))
            {
                previousValue.Sessions.Remove(this);
            }
    
            if (EtatSession != null)
            {
                if (!EtatSession.Sessions.Contains(this))
                {
                    EtatSession.Sessions.Add(this);
                }
                if (EtatSessionsId != EtatSession.Id)
                {
                    EtatSessionsId = EtatSession.Id;
                }
            }
        }
    
        private void FixupVehicules(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Vehicule item in e.NewItems)
                {
                    if (!item.Sessions.Contains(this))
                    {
                        item.Sessions.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Vehicule item in e.OldItems)
                {
                    if (item.Sessions.Contains(this))
                    {
                        item.Sessions.Remove(this);
                    }
                }
            }
        }
    
        private void FixupUtilisateurs_ClientSessions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Utilisateurs_ClientSessions item in e.NewItems)
                {
                    item.Session = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Utilisateurs_ClientSessions item in e.OldItems)
                {
                    if (ReferenceEquals(item.Session, this))
                    {
                        item.Session = null;
                    }
                }
            }
        }

        #endregion
    }
}
