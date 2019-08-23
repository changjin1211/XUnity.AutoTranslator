﻿namespace XUnity.ResourceRedirector
{
   /// <summary>
   /// The operation context surrounding the AssetBundleLoading hook (synchronous) and AsyncAssetBundleLoading hook (asynchronous).
   /// </summary>
   public interface IAssetBundleLoadingContext
   {
      /// <summary>
      /// Gets a normalized path to the asset bundle that is:
      ///  * Relative to the current directory
      ///  * Lower-casing
      ///  * Uses '\' as separators.
      /// </summary>
      /// <returns></returns>
      string GetNormalizedPath();

      /// <summary>
      /// Indicate your work is done and if any other hooks to this asset bundle load should be called.
      /// </summary>
      /// <param name="skipRemainingPrefixes">Indicate if the remaining prefixes should be skipped.</param>
      /// <param name="skipOriginalCall">Indicate if the original call should be skipped. If you set the asset bundle, you likely want to set this to true.</param>
      void Complete( bool skipRemainingPrefixes = true, bool? skipOriginalCall = true );

      /// <summary>
      /// Gets the parameters of the original call.
      /// </summary>
      AssetBundleLoadParameters OriginalParameters { get; }

   }
}