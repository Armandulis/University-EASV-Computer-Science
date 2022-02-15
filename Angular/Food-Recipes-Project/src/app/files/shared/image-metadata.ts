import {FileMetaData} from './file-metadata';

export interface ImageMetadata {
  base64Image?: string;
  imageBlob?: Blob;
  fileMeta: FileMetaData;
}
