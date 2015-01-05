﻿using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.Studio
{
    public class Bank
        : IEquatable<Bank>
    {
        public FMOD.Studio.Bank FmodBank { get; private set; }

        private Bank(FMOD.Studio.Bank bank)
        {
            FmodBank = bank;
        }

        public static Bank FromFmod(FMOD.Studio.Bank bank)
        {
            if (bank == null)
                return null;
            return new Bank(bank);
        }

        #region equality
        public bool Equals(Bank other)
        {
            if (other == null)
                return false;

            return other.FmodBank == FmodBank;
        }

        public override bool Equals(object obj)
        {
            var c = obj as Bank;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodBank != null ? FmodBank.GetHashCode() : 0);
        }
        #endregion

        #region Property access
        public Guid Id
        {
            get
            {
                GUID id;
                FmodBank.getID(out id);
                return id.FromFmod();
            }
        }

        public string Path
        {
            get
            {
                string path;
                FmodBank.getPath(out path).Check();
                return path;
            }
        }

        public void Unload()
        {
            FmodBank.unload();
        }

        public void LoadSampleData()
        {
            FmodBank.loadSampleData();
        }

        public void UnloadSampleData()
        {
            FmodBank.unloadSampleData();
        }

        public LoadingState GetLoadingState()
        {
            LOADING_STATE state;
            FmodBank.getLoadingState(out state).Check();
            return (LoadingState)state;
        }

        public LoadingState GetSampleLoadingState()
        {
            LOADING_STATE state;
            FmodBank.getSampleLoadingState(out state).Check();
            return (LoadingState)state;
        }
        #endregion

        #region Enumeration
        public int StringCount
        {
            get
            {
                int count;
                FmodBank.getStringCount(out count).Check();
                return count;
            }
        }

        public string GetStringInfo(int index, out Guid guid)
        {
            GUID id;
            string path;
            FmodBank.getStringInfo(index, out id, out path).Check();

            guid = id.FromFmod();
            return path;
        }

        public int EventCount
        {
            get
            {
                int count;
                FmodBank.getEventCount(out count).Check();
                return count;
            }
        }

        public IEnumerable<EventDescription> Events
        {
            get
            {
                FMOD.Studio.EventDescription[] evts;
                FmodBank.getEventList(out evts).Check();
                return evts.Select(EventDescription.FromFmod);
            }
        }

        public int BusCount
        {
            get
            {
                int count;
                FmodBank.getBusCount(out count).Check();
                return count;
            }
        }

        public IEnumerable<Bus> Buses
        {
            get
            {
                FMOD.Studio.Bus[] buses;
                FmodBank.getBusList(out buses).Check();
                return buses.Select(Bus.FromFmod);
            }
        }

        public int VCACount
        {
            get
            {
                int count;
                FmodBank.getVCACount(out count).Check();
                return count;
            }
        }

        public IEnumerable<VCA> VCAs
        {
            get
            {
                FMOD.Studio.VCA[] vcas;
                FmodBank.getVCAList(out vcas).Check();
                return vcas.Select(VCA.FromFmod);
            }
        }
        #endregion
    }

    public static class BankExtensions
    {
        public static FMOD.Studio.Bank ToFmod(this Bank bank)
        {
            if (bank == null)
                return null;

            return bank.FmodBank;
        }
    }
}
