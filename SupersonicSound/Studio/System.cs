﻿using System.Collections.Generic;
using System.Linq;
using FMOD;
using FMOD.Studio;
using SupersonicSound.LowLevel;
using SupersonicSound.Wrapper;
using System;
using INITFLAGS = FMOD.Studio.INITFLAGS;

namespace SupersonicSound.Studio
{
    public class System
        : IDisposable
    {
        private const int DEFAULT_MAX_CHANNELS = 1024;
        private const InitFlags DEFAULT_INIT_FLAGS = InitFlags.LiveUpdate;
        private const LowLevelInitFlags DEFAULT_LOW_LEVEL_FLAGS = LowLevelInitFlags.Normal;

        private readonly FMOD.Studio.System _system;

        public LowLevelSystem LowLevelSystem { get; private set; }

        private bool _disposed;

        public System(int maxChannels = DEFAULT_MAX_CHANNELS, InitFlags flags = DEFAULT_INIT_FLAGS, LowLevelInitFlags lowLevelFlags = DEFAULT_LOW_LEVEL_FLAGS, AdvancedInitializationSettings advancedSettings = default(AdvancedInitializationSettings), Action<IPreInitilizeLowLevelSystem> preInit = null)
        {
            //Load native dependencies
            Native.Load();

            //Create studio system
            FMOD.Studio.System.create(out _system).Check();

            //Create low level system
            FMOD.System lowLevel;
            _system.getLowLevelSystem(out lowLevel).Check();
            LowLevelSystem = new LowLevelSystem(lowLevel);

            if (preInit != null)
                preInit(LowLevelSystem);

            //Set advanced settings
            LowLevelSystem.SetAdvancedSettings(advancedSettings);

            //Initialize
            _system.initialize(maxChannels, (INITFLAGS)flags, (FMOD.INITFLAGS)lowLevelFlags, IntPtr.Zero).Check();
        }

        public System(FMOD.Studio.System system)
        {
            //Load native dependencies
            Native.Load();

            _system = system;
        }

        public void Update()
        {
            _system.update().Check();
        }

        #region disposal
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                _system.release().Check();

                _disposed = true;
            }
        }

        ~System()
        {
            Dispose(false);
        }
        #endregion

        //public void GetEvent(string path)
        //{
        //    EventDescription evt;
        //    _system.getEvent(path, out evt).Check();

        //    throw new NotImplementedException("return something");
        //}

        //public void GetEvent(Guid id)
        //{
        //    EventDescription evt;
        //    _system.getEventByID(id.ToFmod(), out evt).Check();

        //    throw new NotImplementedException("return something");
        //}

        public Bus GetBus(string path)
        {
            FMOD.Studio.Bus bus;
            _system.getBus(path, out bus).Check();

            return Bus.FromFmod(bus);
        }

        public Bus GetBus(Guid guid)
        {
            FMOD.Studio.Bus bus;
            _system.getBusByID(guid.ToFmod(), out bus).Check();

            return Bus.FromFmod(bus);
        }

        public VCA GetVCA(string path)
        {
            FMOD.Studio.VCA vca;
            _system.getVCA(path, out vca).Check();

            return VCA.FromFmod(vca);
        }

        public VCA GetVCA(Guid guid)
        {
            FMOD.Studio.VCA vca;
            _system.getVCAByID(guid.ToFmod(), out vca).Check();

            return VCA.FromFmod(vca);
        }

        public Bank GetBank(string path)
        {
            FMOD.Studio.Bank bank;
            _system.getBank(path, out bank).Check();

            return Bank.FromFmod(bank);
        }

        public Bank GetBank(Guid guid)
        {
            FMOD.Studio.Bank bank;
            _system.getBankByID(guid.ToFmod(), out bank).Check();

            return Bank.FromFmod(bank);
        }

        //public RESULT getSoundInfo(string key, out SOUND_INFO info)
        //{
        //    SOUND_INFO_INTERNAL internalInfo;

        //    RESULT result = FMOD_Studio_System_GetSoundInfo(rawPtr, Encoding.UTF8.GetBytes(key + Char.MinValue), out internalInfo);
        //    if (result != RESULT.OK)
        //    {
        //        info = new SOUND_INFO();
        //        return result;
        //    }

        //    internalInfo.assign(out info);

        //    return result;
        //}

        public Guid LookupId(string path)
        {
            GUID guid;
            _system.lookupID(path, out guid).Check();
            return guid.FromFmod();
        }

        public string LookupPath(Guid id)
        {
            string path;
            _system.lookupPath(id.ToFmod(), out path).Check();
            return path;
        }

        public Attributes3D ListenerAttributes
        {
            get
            {
                FMOD.Studio._3D_ATTRIBUTES attr;
                _system.getListenerAttributes(out attr).Check();
                return new Attributes3D(attr);
            }
            set
            {
                _system.setListenerAttributes(value.ToFmod()).Check();
            }
        }

        public Bank LoadBankFromFile(string name, BankLoadingFlags flags)
        {
            FMOD.Studio.Bank bank;
            _system.loadBankFile(name, (LOAD_BANK_FLAGS)flags, out bank).Check();
            return Bank.FromFmod(bank);
        }

        public Bank LoadBankFromMemory(byte[] buffer, BankLoadingFlags flags)
        {
            FMOD.Studio.Bank bank;
            _system.loadBankMemory(buffer, (LOAD_BANK_FLAGS)flags, out bank).Check();
            return Bank.FromFmod(bank);
        }

        //public RESULT loadBankCustom(BANK_INFO info, LOAD_BANK_FLAGS flags, out Bank bank)
        //{
        //    bank = null;

        //    info.size = Marshal.SizeOf(info);

        //    IntPtr newPtr = new IntPtr();
        //    RESULT result = FMOD_Studio_System_LoadBankCustom(rawPtr, ref info, flags, out newPtr);
        //    if (result != RESULT.OK)
        //    {
        //        return result;
        //    }

        //    bank = new Bank(newPtr);
        //    return result;
        //}

        public void UnloadAll()
        {
            _system.unloadAll();
        }

        public void FlushCommands()
        {
            _system.flushCommands();
        }

        //public RESULT startRecordCommands(string path, RECORD_COMMANDS_FLAGS flags)
        //{
        //    return FMOD_Studio_System_StartRecordCommands(rawPtr, Encoding.UTF8.GetBytes(path + Char.MinValue), flags);
        //}

        public void StopRecordedCommands()
        {
            _system.stopRecordCommands();
        }

        public void PlaybackCommands(string path)
        {
            _system.playbackCommands(path).Check();
        }

        public int BankCount
        {
            get
            {
                int count;
                _system.getBankCount(out count).Check();
                return count;
            }
        }

        public IEnumerable<Bank> Banks
        {
            get
            {
                FMOD.Studio.Bank[] banks;
                _system.getBankList(out banks).Check();

                return banks.Select(Bank.FromFmod);
            }
        }

        public CpuUsage GetCPUUsage()
        {
            CPU_USAGE usage;
            _system.getCPUUsage(out usage).Check();
            return new CpuUsage(usage);
        }

        public BufferUsage GetBufferUsage()
        {
            BUFFER_USAGE buffer;
            _system.getBufferUsage(out buffer).Check();
            return new BufferUsage(buffer);
        }

        public void ResetBufferUsage()
        {
            _system.resetBufferUsage();
        }

        //public RESULT setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask)
        //{
        //    return FMOD_Studio_System_SetCallback(rawPtr, callback, callbackmask);
        //}

        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                _system.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _system.setUserData(value).Check();
            }
        }
    }
}
