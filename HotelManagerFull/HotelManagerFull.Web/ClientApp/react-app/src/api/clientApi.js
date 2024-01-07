import axiosClient from "./axiosClient";

class ClientApi {
  getAllProvinces = () => {
    const url = "/client/GetAllProvinces";
    return axiosClient.get(url);
  };

  getAllGallery = () => {
    const url = "/client/GetAllGallery";
    return axiosClient.get(url);
  };

  getAllHotelByProvinces = (params) => {
    const url = "/client/GetAllHotelByProvinces";
    return axiosClient.get(url, { params });
  };

  getAllRoomByHotel = (params) => {
    const url = "/client/GetAllRoomByHotel";
    return axiosClient.get(url, { params });
  };

  getAllRoomDetail = (params) => {
    const url = "/client/GetAllRoomDetail";
    return axiosClient.get(url, { params });
  };
}

const clientApi = new ClientApi();
export default clientApi;
