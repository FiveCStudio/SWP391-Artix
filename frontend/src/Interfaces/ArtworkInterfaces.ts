export interface Artwork{
  artworkID: number,
  creatorID: number,
  artworkName: string,
  description: string,
  dateCreated: Date,
  likes: number,
  purchasable: false,
  price: number,
  imageFile: string | null,
  artworkTag: [
    {
      "artworkTagID": number,
      "artworkID": number,
      "tagID": number
    },
  ]
}