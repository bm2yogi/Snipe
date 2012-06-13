using System;
using System.Linq;
using System.Text;
using SnipeLib;

namespace Snipe
{
    public interface IContextSpecBuilder
    {
        void Build(ISpecFile specFile);
    }

    public partial class ContextSpecBuilder : IContextSpecBuilder
    {
        private readonly IContextBuilder _contextBuilder;
        private readonly IScenarioBuilder _scenarioBuilder;

        public ContextSpecBuilder(IContextBuilder contextBuilder, IScenarioBuilder scenarioBuilder)
        {
            _contextBuilder = contextBuilder;
            _scenarioBuilder = scenarioBuilder;
        }

        public void Build(ISpecFile specFile)
        {
            specFile.Contexts
                .Values
                .ToList()
                .ForEach(context =>
                             {
                                 _contextBuilder.BuildContext(context);
                                 context.Scenarios
                                     .ToList()
                                     .ForEach(scenario => _scenarioBuilder.BuildScenario(scenario, context));
                             });
        }
    }
}