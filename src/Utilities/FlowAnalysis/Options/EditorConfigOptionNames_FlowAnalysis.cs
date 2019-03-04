﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace Analyzer.Utilities
{
    /// <summary>
    /// Option names to configure analyzer execution through an .editorconfig file.
    /// </summary>
    internal static partial class EditorConfigOptionNames
    {
        // =============================================================================================================
        // NOTE: Keep this file in sync with documentation at '<%REPO_ROOT%>\docs\Analyzer Configuration.md'
        // =============================================================================================================

        #region Flow analysis options

        /// <summary>
        /// Option to configure interprocedural dataflow analysis kind, i.e. <see cref="Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.InterproceduralAnalysisKind"/>.
        /// Allowed option values: Fields from <see cref="Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.InterproceduralAnalysisKind"/>.
        /// </summary>
        public const string InterproceduralAnalysisKind = "interprocedural_analysis_kind";

        /// <summary>
        /// Integral option to configure maximum method call chain for interprocedural dataflow analysis.
        /// </summary>
        public const string MaxInterproceduralMethodCallChain = "max_interprocedural_method_call_chain";

        /// <summary>
        /// Integral option to configure maximum lambda or local function call chain for interprocedural dataflow analysis.
        /// </summary>
        public const string MaxInterproceduralLambdaOrLocalFunctionCallChain = "max_interprocedural_lambda_or_local_function_call_chain";

        /// <summary>
        /// String option to configure dispose analysis kind, primarily for CA2000 (DisposeObjectsBeforeLosingScope).
        /// Allowed option values: Fields from DisposeAnalysisKind enum>.
        /// </summary>
        public const string DisposeAnalysisKind = "dispose_analysis_kind";

        /// <summary>
        /// Option to configure whether copy analysis should be executed during dataflow analysis.
        /// Copy analysis tracks value and reference copies. For example,
        /// <code>
        ///     void M(string str1, string str2)
        ///     {
        ///         if (str1 != null && str1 == str2)
        ///         {
        ///             if (str2 != null) // This is redundant as 'str1' and 'str2' are value copies on this code path. This requires copy analysis.
        ///         }
        ///     }
        /// </code>
        /// </summary>
        public const string CopyAnalysis = "copy_analysis";

        #endregion
    }
}
