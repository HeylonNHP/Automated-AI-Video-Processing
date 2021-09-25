﻿using System;

namespace Automated_AI_Video_Processing.AiProcessors.RCG
{
    public struct InterpolationFactorOptions
    {
        public int mode;
        public int? interpolationFactor;
        public int? outputFPS;
    }
    public class RifeColabGuiSettings
    {
        private string inputFileInternal;
        private InterpolationFactorOptions _interpolationFactorOptions;
        private bool loopable;
        private int[] gpuIds;
        private int batchSize;
        private bool autoEncode;
        
        public RifeColabGuiSettings(string inputFile,
            InterpolationFactorOptions interpolationFactorOptions,
            bool loopable = false,
            int[] gpuIds = null,
            int batchSize = 1,
            bool autoEncode = true)
        {
            inputFileInternal = inputFile;
            if (gpuIds == null)
            {
                gpuIds = new int[] { 0 };
            }

            this.inputFileInternal = inputFile;
            this._interpolationFactorOptions = interpolationFactorOptions;
            this.loopable = loopable;
            this.gpuIds = gpuIds;
            this.batchSize = 1;
            this.autoEncode = autoEncode;
        }

        public string InputFileInternal
        {
            get => inputFileInternal;
            set => inputFileInternal = value;
        }

        public InterpolationFactorOptions InterpolationFactorOptions
        {
            get => _interpolationFactorOptions;
            set => _interpolationFactorOptions = value;
        }

        public bool Loopable
        {
            get => loopable;
            set => loopable = value;
        }

        public int[] GpuIds
        {
            get => gpuIds;
            set => gpuIds = value;
        }

        public int BatchSize
        {
            get => batchSize;
            set => batchSize = value;
        }

        public bool AutoEncode
        {
            get => autoEncode;
            set => autoEncode = value;
        }

        public override string ToString()
        {
            string outputCommandLineArgs = "";
            if (inputFileInternal != null)
            {
                outputCommandLineArgs += $"-i {inputFileInternal} ";
            }

            if (_interpolationFactorOptions.interpolationFactor != null)
            {
                outputCommandLineArgs += $"-if {_interpolationFactorOptions.interpolationFactor} ";
            }

            if (_interpolationFactorOptions.outputFPS != null)
            {
                throw new NotImplementedException();
            }

            outputCommandLineArgs += $"-mode {_interpolationFactorOptions.mode} ";

            if (loopable)
            {
                outputCommandLineArgs += $"-loop {loopable} ";
            }

            if (gpuIds != null)
            {
                outputCommandLineArgs += $"-gpuids {string.Join(",",gpuIds)} ";
            }

            outputCommandLineArgs += $"-batch {batchSize} ";

            if (autoEncode)
            {
                outputCommandLineArgs += $"-autoencode {autoEncode} ";
            }

            return outputCommandLineArgs.Trim();
        }
    }
}