using System.IO;
using Exa.IO;
using Exa.Types.Binding;
using Exa.Types.Generics;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints {
    public class BlueprintContainer : Observable<Blueprint>, ISerializableItem, IKeySelector<string> {
        public BlueprintContainer(BlueprintContainerArgs args)
            : base(args.blueprint) {
            if (args.generateBlueprintFileHandle) {
                BlueprintFileHandle = new FileHandle(
                    this,
                    name => Tree.Root.Blueprints.CombineWith($"{name}.json"),
                    path => IOUtils.JsonSerializeToPath(Data, path),
                    args.generateBlueprintFileName
                );
            }

            var thumbnailDirectory = args.useDefaultThumbnailFolder
                ? Tree.Root.DefaultThumbnails
                : Tree.Root.Thumbnails;

            ThumbnailFileHandle = new FileHandle(
                this,
                name => thumbnailDirectory.CombineWith($"{name}.png"),
                path => {
                    if (Data.Thumbnail != null) {
                        IOUtils.SaveTexture2D(Data.Thumbnail, path);
                    }
                }
            );
        }

        [JsonIgnore] public FileHandle BlueprintFileHandle { get; set; }
        [JsonIgnore] public FileHandle ThumbnailFileHandle { get; set; }

        [JsonIgnore] public string Key {
            get => Data.name;
        }

        [JsonIgnore] public string ItemName {
            get => Data.name;
        }

        public override void SetData(Blueprint data, bool notify = true) {
            Data = data;
            
            // Generate a thumbnail with the new blueprint.
            if (notify) {
                S.Thumbnails.GenerateThumbnail(data);
                
                // Make sure that Data is set before the thumbnail file handle is refreshed, as it requires the Data 
                ThumbnailFileHandle.Refresh();
                BlueprintFileHandle.Refresh();
                
                Notify();
            }
        }

        public void LoadThumbnail() {
            if (File.Exists(ThumbnailFileHandle.TargetPath)) {
                Data.Thumbnail = IOUtils.LoadTexture2D(ThumbnailFileHandle.TargetPath, 512, 512);
            } else {
                S.Thumbnails.GenerateThumbnail(Data);
                ThumbnailFileHandle.Refresh();
            }
        }
    }
}