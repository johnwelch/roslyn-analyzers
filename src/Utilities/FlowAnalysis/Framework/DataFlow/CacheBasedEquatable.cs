﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Analyzer.Utilities;

namespace Microsoft.CodeAnalysis.FlowAnalysis.DataFlow
{
    /// <summary>
    /// Abstract cache based equatable implementation for objects that are compared frequently and hence need a performance optimization of using a cached hash code.
    /// </summary>
    internal abstract class CacheBasedEquatable<T> : IEquatable<T>
        where T: class
    {
        private ImmutableArray<int> _lazyHashCodeParts;
        private int _lazyHashCode;

        protected CacheBasedEquatable()
        {
        }

        private int GetOrComputeHashCode()
        {
            if (_lazyHashCodeParts.IsDefault)
            {
                var hashCodeParts = ComputeHashCodeParts();
                var hashCode = HashUtilities.Combine(hashCodeParts, GetType().GetHashCode());

                if (_lazyHashCodeParts.IsDefault)
                {
                    lock (this)
                    {
                        _lazyHashCode = hashCode;
                        _lazyHashCodeParts = hashCodeParts;
                    }
                }
            }

            return _lazyHashCode;
        }

        private ImmutableArray<int> ComputeHashCodeParts()
        {
            var builder = ArrayBuilder<int>.GetInstance();
            ComputeHashCodeParts(builder);
            return builder.ToImmutableAndFree();
        }

        protected abstract void ComputeHashCodeParts(ArrayBuilder<int> builder);

        public sealed override int GetHashCode() => GetOrComputeHashCode();

        public sealed override bool Equals(object obj) => Equals(obj as T);
        public bool Equals(T other)
        {
            // Perform fast equality checks first.
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var otherEquatable = other as CacheBasedEquatable<T>;
            if (otherEquatable == null || GetHashCode() != otherEquatable.GetHashCode())
            {
                return false;
            }

            // Now perform slow check that compares individual hash code parts sequences.
            Debug.Assert(!_lazyHashCodeParts.IsDefault);
            Debug.Assert(!otherEquatable._lazyHashCodeParts.IsDefault);
            return _lazyHashCodeParts.SequenceEqual(otherEquatable._lazyHashCodeParts);
        }

        public static bool operator ==(CacheBasedEquatable<T> value1, CacheBasedEquatable<T> value2)
        {
            if ((object)value1 == null)
            {
                return (object)value2 == null;
            }
            else if ((object)value2 == null)
            {
                return false;
            }

            return value1.Equals(value2);
        }

        public static bool operator !=(CacheBasedEquatable<T> value1, CacheBasedEquatable<T> value2)
        {
            return !(value1 == value2);
        }
    }
}