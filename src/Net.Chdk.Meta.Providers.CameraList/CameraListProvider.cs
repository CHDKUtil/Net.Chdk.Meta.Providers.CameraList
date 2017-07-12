using Net.Chdk.Meta.Model.CameraList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Meta.Providers.CameraList
{
    sealed class CameraListProvider : ICameraListProvider
    {
        private IEnumerable<IInnerCameraListProvider> InnerProviders;

        public CameraListProvider(IEnumerable<IInnerCameraListProvider> innerProviders)
        {
            InnerProviders = innerProviders;
        }

        public IDictionary<string, ListPlatformData> GetCameraList(string path, string categoryName)
        {
            var ext = Path.GetExtension(path);
            var writer = InnerProviders.SingleOrDefault(w => w.Extension.Equals(ext, StringComparison.OrdinalIgnoreCase));
            if (writer == null)
                throw new InvalidOperationException($"Unknown camera writer extension: {ext}");
            return writer.GetCameraList(path, categoryName);
        }
    }
}
