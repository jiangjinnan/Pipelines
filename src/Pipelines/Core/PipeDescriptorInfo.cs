using System.Text;

namespace Artech.Pipelines
{
    /// <summary>
    /// Represents the pipe descriptive object.
    /// </summary>
    public class PipeDescriptorInfo
    {
        #region Fields
        private PipeDescriptorInfo? _next;
        private PipeDescriptorInfo? _sub;
        private string? _exportedDescription;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the singleton <see cref="PipeDescriptorInfo"/> describing the terminal pipe.
        /// </summary>
        public static PipeDescriptorInfo Terminal { get; } = new("Terminal");

        /// <summary>
        /// Gets pipe descriptive information
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the <see cref="PipeDescriptorInfo"/> describing the next pipe in the pipeline.
        /// </summary>
        public PipeDescriptorInfo? Next { get => _next; set { _next = value; _exportedDescription = null; } }

        /// <summary>
        /// Gets or sets the <see cref="PipeDescriptorInfo"/> describing the sub-pipeline.
        /// </summary>
        public PipeDescriptorInfo? SubPipeline { get => _sub; set { _sub = value; _exportedDescription = null; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="PipeDescriptorInfo"/>.
        /// </summary>
        /// <param name="description">Pipe descriptive information.</param>
        /// <param name="next">The <see cref="PipeDescriptorInfo"/> describing the next pipe in the pipeline.</param>
        public PipeDescriptorInfo(string description, PipeDescriptorInfo? next = null)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Next = next;
        }
        #endregion

        #region Public methods
        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (_exportedDescription != null)
            {
                return _exportedDescription;
            }
            var builder = new StringBuilder();
            Format().ForEach(it => builder.AppendLine(it));
            return builder.ToString();
        }
        #endregion

        #region Private methods
        private List<string> Format()
        {
            var exportedList = new List<string>();
            Walk(exportedList, 0, 1, this);
            return exportedList;

            static void Walk(List<string> exported, int indent, int counter, PipeDescriptorInfo? current)
            {
                if (current == null || current == Terminal)
                {
                    return;
                }
                exported.Add($"{new string('\t', indent)}{counter}.{current.Description}");
                var subPipeline = current.SubPipeline;
                if (subPipeline != null)
                {
                    foreach (var sub in subPipeline.Format())
                    {
                        exported.Add($"{new string('\t', indent + 1)}{sub}");
                    }
                }
                Walk(exported, indent, counter + 1, current.Next);
            }
        }
        #endregion
    }
}