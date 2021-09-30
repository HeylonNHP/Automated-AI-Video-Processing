using System;

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
        private string _inputFile;
        private InterpolationFactorOptions _interpolationFactorOptions;
        private bool loopable;
        private int[] gpuIds;
        private int batchSize;
        private bool autoEncode;
        private int maxBatchThreadRestarts;
        private bool exitOnMaxBatchThreadRestarts;
        
        public RifeColabGuiSettings(string inputFile,
            InterpolationFactorOptions interpolationFactorOptions,
            bool loopable = false,
            int[] gpuIds = null,
            int batchSize = 1,
            bool autoEncode = true,
            int maxBatchThreadRestarts = 20, bool exitOnMaxBatchThreadRestarts = true)
        {
            _inputFile = inputFile;
            if (gpuIds == null)
            {
                gpuIds = new int[] { 0 };
            }

            this._inputFile = inputFile;
            this._interpolationFactorOptions = interpolationFactorOptions;
            this.loopable = loopable;
            this.gpuIds = gpuIds;
            this.batchSize = 1;
            this.autoEncode = autoEncode;
            this.maxBatchThreadRestarts = maxBatchThreadRestarts;
            this.exitOnMaxBatchThreadRestarts = exitOnMaxBatchThreadRestarts;
        }

        public string InputFile
        {
            get => _inputFile;
            set => _inputFile = value;
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
            if (_inputFile != null)
            {
                outputCommandLineArgs += $"-i {_inputFile} ";
            }

            if (_interpolationFactorOptions.interpolationFactor != null)
            {
                outputCommandLineArgs += $"-if {_interpolationFactorOptions.interpolationFactor} ";
            }

            if (_interpolationFactorOptions.outputFPS != null)
            {
                outputCommandLineArgs += $"-targetfpsmode {true} -targetfps {_interpolationFactorOptions.outputFPS} ";
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

            outputCommandLineArgs += $"-maxBatchBackupThreadRestarts {maxBatchThreadRestarts} ";

            outputCommandLineArgs += $"-exitOnMaxBatchBackupThreadRestarts {exitOnMaxBatchThreadRestarts} ";

            return outputCommandLineArgs.Trim();
        }

        public RifeColabGuiSettings Clone()
        {
            RifeColabGuiSettings newSettings = new RifeColabGuiSettings((string)(_inputFile != null? _inputFile.Clone():null),
                new InterpolationFactorOptions()
                {
                    mode = _interpolationFactorOptions.mode,
                    interpolationFactor = _interpolationFactorOptions.interpolationFactor,
                    outputFPS = _interpolationFactorOptions.outputFPS,
                },
                loopable, (int[])gpuIds.Clone(), batchSize, autoEncode,maxBatchThreadRestarts);
            return newSettings;
        }
    }
}