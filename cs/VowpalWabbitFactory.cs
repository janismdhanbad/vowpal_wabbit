﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VowpalWabbitFactory.cs">
//   Copyright (c) by respective owners including Yahoo!, Microsoft, and
//   individual contributors. All rights reserved.  Released under a BSD
//   license as described in the file LICENSE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VW.Interfaces;
using VW.Serializer;

namespace VW
{
    public class VowpalWabbitPredictionManager : IDisposable
    {
        private ObjectPool<VowpalWabbitModel, VowpalWabbitNative> vwPool;

        public VowpalWabbitPredictionManager(VowpalWabbitSettings settings)
        {
            this.Settings = settings;
            this.vwPool = new ObjectPool<VowpalWabbitModel, VowpalWabbitNative>();
        }

        public VowpalWabbitSettings Settings
        {
            get;
            private set;
        }

        public VowpalWabbit<TExample> GetOrCreate<TExample>()
        {
            typeof(TExample)
        }

        public void UpdateModel(Stream model)
        {
            var modelSettings = new VowpalWabbitSettings
            {
                Arguments = this.Settings.Arguments,
                ModelStream = model
            };

            this.UpdateModel(new VowpalWabbitModel(modelSettings));
        }

        public void UpdateModel(string filename)
        {
            var modelSettings = this.Settings.ShallowCopy();

            modelSettings.Arguments += " -i " + filename;
            modelSettings.ParallelOptions = null;


            this.UpdateModel(new VowpalWabbitModel(modelSettings));
        }

        public void UpdateModel(VowpalWabbitModel model)
        {
            var childSettings = model.ShallowCopy();
            childSettings.Model = model;

            this.vwPool.UpdateFactory(ObjectFactory.Create(model, model => new VowpalWabbitNative(childSettings)));
        }
    }

    public static class VowpalWabbitFactory
    {
        public static VowpalWabbitNative Create(VowpalWabbitSettings settings)
        {
            //if (settings.ParallelOptions == null)
            //{
            //    return new VowpalWabbit(settings);
            //}
            //else 
            return null;
        }

        public static VowpalWabbit<TExample> Create<TExample>(VowpalWabbitSettings settings)
        {
            return null;
        }

        public static VowpalWabbit<TExample, TActionDependentFeature> Create<TExample, TActionDependentFeature>(VowpalWabbitSettings settings)
        {
            return null;

        }

        public static VowpalWabbitManager Create(VowpalWabbitSettings settings)
        {
            return new VowpalWabbitManager(settings);
        }

        //    where TExample : SharedExample, IActionDependentFeatureExample<TActionDependentFeature>
        //    where TActionDependentFeature : IExample;

        //public IVowpalWabbitPredictor CreatePredictor(VowpalWabbitSettings settings);

        
        //    where TExample : IExample;

        //public IVowpalWabbitPredictor<TExample> CreatePredictor<TExample>(VowpalWabbitSettings settings);

        //public IVowpalWabbit<TExample, TActionDependentFeature> Create<TExample, TActionDependentFeature>(VowpalWabbitSettings settings)
        //    where TExample : SharedExample, IActionDependentFeatureExample<TActionDependentFeature>
        //    where TActionDependentFeature : IExample;

        //public IVowpalWabbitPredictor<TExample, TActionDependentFeature> CreatePredictor<TExample, TActionDependentFeature>(VowpalWabbitSettings settings)
        //    where TExample : SharedExample, IActionDependentFeatureExample<TActionDependentFeature>;

    }



    //public interface VowpalWabbitFactor2
    //{

    //    /*
    //     * Instances[]
    //     * 
    //     * Instance[i].Learn()
    //     * 
    //     * EndOfPass()
    //     * 
    //     * 
    //     *  Create[].
    //     *  
    //     *  queue<avaialble instances>
    //     *  
    //     * Learn()
    //     * {
    //     *  ThreadLocalStorage
    //     *  
    //     *  lock(queue)
    //     *  {
    //     *      picke fre()
    //     *  }
    //     *  
    //     * learn
    //     * 
    //     * lock (queue)
    //     * {
    //     * return
    //     * }
    //     */
    //}

    ///// <summary>
    ///// <see cref="IObjectFactory{VowpalWabbit}"/> implementation to produce <see cref="VowpalWabbit"/> instances.
    ///// </summary>
    //public class VowpalWabbitFactory : VowpalWabbitFactoryBase<VowpalWabbit>
    //{
    //    /// <summary>
    //    /// Initializes a new <see cref="VowpalWabbitFactory"/> instance.
    //    /// </summary>
    //    /// <param name="model">The shared model.</param>
    //    public VowpalWabbitFactory(VowpalWabbitModel model) : base(model)
    //    {
    //    }

    //    /// <summary>
    //    /// Creates a new <see cref="VowpalWabbit"/> instance using the shared model.
    //    /// </summary>
    //    /// <returns>A new <see cref="VowpalWabbit"/> instance.</returns>
    //    public override VowpalWabbit Create()
    //    {
    //        return new VowpalWabbit(this.model);
    //    }
    //}

    ///// <summary>
    ///// <see cref="IObjectFactory{TVowpalWabbit}"/> implementation to produce <see cref="VowpalWabbit{TExample}"/> instances.
    ///// </summary>
    //public class VowpalWabbitFactory<TExample> : VowpalWabbitFactoryBase<VowpalWabbit<TExample>>
    //{
    //    /// <summary>
    //    /// Serializer settings.
    //    /// </summary>
    //    private VowpalWabbitSettings settings;

    //    /// <summary>
    //    /// Initializes a new <see cref="VowpalWabbitFactory{TExample}"/> instance.
    //    /// </summary>
    //    /// <param name="model">The shared model.</param>
    //    /// <param name="settings">The serializer settings.</param>
    //    public VowpalWabbitFactory(VowpalWabbitModel model, VowpalWabbitSettings settings = null)
    //        : base(model)
    //    {
    //        this.settings = settings;
    //    }

    //    /// <summary>
    //    /// Creates a new <see cref="VowpalWabbit{TExample}"/> instance using the shared model.
    //    /// </summary>
    //    /// <returns>A new <see cref="VowpalWabbit{TExample}"/> instance.</returns>
    //    public override VowpalWabbit<TExample> Create()
    //    {
    //        return new VowpalWabbit<TExample>(this.model, this.settings);
    //    }
    //}

    ///// <summary>
    ///// <see cref="IObjectFactory{TVowpalWabbit}"/> implementation to produce <see cref="VowpalWabbitPredictor{TExample,TActionDependentFeature}"/> instances.
    ///// </summary>
    //public class VowpalWabbitPredictorFactory<TExample, TActionDependentFeature> : VowpalWabbitFactoryBase<VowpalWabbitPredictor<TExample, TActionDependentFeature>>
    //    where TExample : SharedExample, IActionDependentFeatureExample<TActionDependentFeature>
    //{
    //    /// <summary>
    //    /// Serializer settings.
    //    /// </summary>
    //    private VowpalWabbitSettings settings;

    //    /// <summary>
    //    /// Initializes a new <see cref="VowpalWabbitFactory{TExample, TActionDependentFeature}"/> instance.
    //    /// </summary>
    //    /// <param name="model">The shared model.</param>
    //    /// <param name="settings">The serializer settings.</param>
    //    public VowpalWabbitPredictorFactory(VowpalWabbitModel model, VowpalWabbitSettings settings = null)
    //        : base(model)
    //    {
    //        this.settings = settings;
    //    }

    //    /// <summary>
    //    /// Creates a new <see cref="VowpalWabbit{TExample,TActionDependentFeature}"/> instance using the shared model.
    //    /// </summary>
    //    /// <returns>A new <see cref="VowpalWabbit{TExample,TActionDependentFeature}"/> instance.</returns>
    //    public override VowpalWabbitPredictor<TExample, TActionDependentFeature> Create()
    //    {
    //        return new VowpalWabbitPredictor<TExample, TActionDependentFeature>(this.model, this.settings);
    //    }
    //}

    ///// <summary>
    ///// <see cref="IObjectFactory{TVowpalWabbit}"/> implementation to produce <see cref="VowpalWabbit{TExample,TActionDependentFeature}"/> instances.
    ///// </summary>
    //public class VowpalWabbitFactory<TExample, TActionDependentFeature> : VowpalWabbitFactoryBase<VowpalWabbit<TExample, TActionDependentFeature>>
    //    where TExample : SharedExample, IActionDependentFeatureExample<TActionDependentFeature>
    //    where TActionDependentFeature : IExample
    //{
    //    /// <summary>
    //    /// Serializer settings.
    //    /// </summary>
    //    private VowpalWabbitSettings settings;

    //    /// <summary>
    //    /// Initializes a new <see cref="VowpalWabbitFactory{TExample, TActionDependentFeature}"/> instance.
    //    /// </summary>
    //    /// <param name="model">The shared model.</param>
    //    /// <param name="settings">The serializer settings.</param>
    //    public VowpalWabbitFactory(VowpalWabbitModel model, VowpalWabbitSettings settings = null)
    //        : base(model)
    //    {
    //        this.settings = settings;
    //    }

    //    /// <summary>
    //    /// Creates a new <see cref="VowpalWabbit{TExample,TActionDependentFeature}"/> instance using the shared model.
    //    /// </summary>
    //    /// <returns>A new <see cref="VowpalWabbit{TExample,TActionDependentFeature}"/> instance.</returns>
    //    public override VowpalWabbit<TExample, TActionDependentFeature> Create()
    //    {
    //        return new VowpalWabbit<TExample, TActionDependentFeature>(this.model, this.settings);
    //    }
    //}
}
