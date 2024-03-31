import axios from "axios";

const arturl = "https://localhost:7233/api/artworks";
const arturlVnpayPayment = "https://localhost:7233";

export const getArtWithStatus = (id, pageNumber) => {
  return axios.get(
    arturl +
      "/GetArtworksWithPaymentStatus/" +
      id +
      `?pageNumber=${pageNumber || 1}&pageSize=6`
  );
};

export const getArtDetail = (id) => {
  return axios.get(arturl + "/" + id);
};

export const VnpayPayment = (payload) => {
  return axios.post(arturlVnpayPayment + "/Payment", payload);
};
