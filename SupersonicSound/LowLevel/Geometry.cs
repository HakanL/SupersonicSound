﻿using System.Numerics;
using FMOD;
using SupersonicSound.Extensions;
using SupersonicSound.Wrapper;
using System;
using System.Linq;

namespace SupersonicSound.LowLevel
{
    public struct Geometry
        : IEquatable<Geometry>//, IHandle
    {
        private readonly FMOD.Geometry _fmodGeometry;

        public FMOD.Geometry FmodGeometry
        {
            get
            {
                return _fmodGeometry;
            }
        }

        public Geometry(FMOD.Geometry geometry)
            : this()
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");
            _fmodGeometry = geometry;
        }

        //public bool IsValid()
        //{
        //    return FmodGeometry.isValid();
        //}

        #region equality
        public bool Equals(Geometry other)
        {
            return other._fmodGeometry == _fmodGeometry;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Geometry))
                return false;

            return Equals((Geometry)obj);
        }

        public override int GetHashCode()
        {
            return (_fmodGeometry != null ? _fmodGeometry.GetHashCode() : 0);
        }
        #endregion

        #region Polygon manipulation.
        public int AddPolygon(float directOcclusion, float reverbOcclusion, bool doubleSided, Vector3[] vertices)
        {
            int index;
            _fmodGeometry.addPolygon(directOcclusion, reverbOcclusion, doubleSided, vertices.Length, vertices.Select(a => a.ToFmod()).ToArray(), out index).Check();
            return index;
        }

        public int PolygonCount
        {
            get
            {
                int num;
                _fmodGeometry.getNumPolygons(out num).Check();
                return num;
            }
        }

        public int MaxPolygons
        {
            get
            {
                int maxvertices;
                int maxpolygons;
                _fmodGeometry.getMaxPolygons(out maxpolygons, out maxvertices).Check();
                return maxpolygons;
            }
        }

        public int MaxVertices
        {
            get
            {
                int maxvertices;
                int maxpolygons;
                _fmodGeometry.getMaxPolygons(out maxpolygons, out maxvertices).Check();
                return maxvertices;
            }
        }

        public struct PolygonCollection
        {
            private readonly FMOD.Geometry _fmodGeometry;

            public Poly this[int index]
            {
                get
                {
                    return new Poly(index, _fmodGeometry);
                }
            }

            public PolygonCollection(FMOD.Geometry geometry)
            {
                _fmodGeometry = geometry;
            }
        }

        public struct Poly
        {
            public readonly int Index;
            private readonly FMOD.Geometry _fmodGeometry;

            public Poly(int index, FMOD.Geometry fmodGeometry)
            {
                Index = index;
                _fmodGeometry = fmodGeometry;
            }

            public int VertexCount
            {
                get
                {
                    int num;
                    _fmodGeometry.getPolygonNumVertices(Index, out num).Check();
                    return num;
                }
            }

            public Vector3 this[int vertexIndex]
            {
                get
                {
                    VECTOR vector;
                    _fmodGeometry.getPolygonVertex(Index, vertexIndex, out vector).Check();
                    return vector.FromFmod();
                }
                set
                {
                    VECTOR vector = value.ToFmod();
                    _fmodGeometry.setPolygonVertex(Index, vertexIndex, ref vector).Check();
                }
            }

            public float DirectOcclusion
            {
                get
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);
                    return d;
                }
                set
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);

                    SetAttributes(value, r, s);
                }
            }

            public float ReverbOcclusion
            {
                get
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);
                    return r;
                }
                set
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);

                    SetAttributes(d, value, s);
                }
            }

            public bool DoubleSided
            {
                get
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);
                    return s;
                }
                set
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);

                    SetAttributes(d, r, value);
                }
            }

            public void GetAttributes(out float directOcclusion, out float reverbOcclusion, out bool doubleSided)
            {
                _fmodGeometry.getPolygonAttributes(Index, out directOcclusion, out reverbOcclusion, out doubleSided).Check();
            }

            public void SetAttributes(float directOcclusion, float reverbOcclusion, bool doubleSided)
            {
                if (directOcclusion < 0 || directOcclusion > 1)
                    throw new ArgumentOutOfRangeException("directOcclusion", "direct occlusion must be 0 < Direct < 1");
                if (reverbOcclusion < 0 || reverbOcclusion > 1)
                    throw new ArgumentOutOfRangeException("reverbOcclusion", "reverb occlusion must be 0 < Reverb < 1");

                _fmodGeometry.setPolygonAttributes(Index, directOcclusion, reverbOcclusion, doubleSided).Check();
            }
        }

        public PolygonCollection Polygon
        {
            get
            {
                return new PolygonCollection(FmodGeometry);
            }
        }
        #endregion

        #region Object manipulation.
        public bool Active
        {
            get
            {
                bool active;
                _fmodGeometry.getActive(out active).Check();
                return active;
            }
            set
            {
                _fmodGeometry.setActive(value).Check();
            }
        }

        public void SetRotation(Vector3 forward, Vector3 up)
        {
            VECTOR f = forward.ToFmod();
            VECTOR u = up.ToFmod();
            _fmodGeometry.setRotation(ref f, ref u).Check();
        }

        public void GetRotation(out Vector3 forward, out Vector3 up)
        {
            VECTOR f;
            VECTOR u;
            _fmodGeometry.getRotation(out f, out u).Check();

            forward = f.FromFmod();
            up = u.FromFmod();
        }

        /// <summary>
        /// Set rotation from quaternion values
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetRotation(float w, float x, float y, float z)
        {
            // http://nic-gamedev.blogspot.co.uk/2011/11/quaternion-math-getting-local-axis.html

            var f = new VECTOR {
                x = 2 * (x * z + w * y),
                y = 2 * (y * x - w * x),
                z = 1 - 2 * (x * x + y * y)
            };

            var u = new VECTOR {
                x = 2 * (x * y - w * z), 
                y = 1 - 2 * (x * x + z * z),
                z = 2 * (y * z + w * x)
            };

            _fmodGeometry.setRotation(ref f, ref u).Check();
        }

        public Vector3 Position
        {
            get
            {
                VECTOR position;
                _fmodGeometry.getPosition(out position).Check();
                return position.FromFmod();
            }
            set
            {
                var pos = value.ToFmod();
                _fmodGeometry.setPosition(ref pos).Check();
            }
        }

        public Vector3 Scale
        {
            get
            {
                VECTOR scale;
                _fmodGeometry.getScale(out scale).Check();
                return scale.FromFmod();
            }
            set
            {
                var scale = value.ToFmod();
                _fmodGeometry.setScale(ref scale).Check();
            }
        }

        public ArraySegment<byte> Save()
        {
            int size;
            _fmodGeometry.save(IntPtr.Zero, out size).Check();

            byte[] buffer = new byte[size];
            unsafe
            {
                fixed (byte* ptr = &buffer[0])
                {
                    _fmodGeometry.save(new IntPtr(ptr), out size).Check();
                }
            }

            return new ArraySegment<byte>(buffer, 0, size);
        }
        #endregion

        #region Userdata set/get.
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                _fmodGeometry.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                _fmodGeometry.setUserData(value).Check();
            }
        }
        #endregion
    }
}
