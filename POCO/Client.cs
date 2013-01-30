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
    public partial class Client : Utilisateur
    {
        #region Primitive Properties
    
        public virtual System.DateTime DateNaissance
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IsCoded
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Offre> Offres
        {
            get
            {
                if (_offres == null)
                {
                    var newCollection = new FixupCollection<Offre>();
                    newCollection.CollectionChanged += FixupOffres;
                    _offres = newCollection;
                }
                return _offres;
            }
            set
            {
                if (!ReferenceEquals(_offres, value))
                {
                    var previousValue = _offres as FixupCollection<Offre>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOffres;
                    }
                    _offres = value;
                    var newValue = value as FixupCollection<Offre>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOffres;
                    }
                }
            }
        }
        private ICollection<Offre> _offres;
    
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
    
        private void FixupOffres(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Offre item in e.NewItems)
                {
                    item.Client = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Offre item in e.OldItems)
                {
                    if (ReferenceEquals(item.Client, this))
                    {
                        item.Client = null;
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
                    item.Utilisateurs_Client = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Utilisateurs_ClientSessions item in e.OldItems)
                {
                    if (ReferenceEquals(item.Utilisateurs_Client, this))
                    {
                        item.Utilisateurs_Client = null;
                    }
                }
            }
        }

        #endregion
    }
}