import React, { useEffect, useState } from "react";
import { loadJs } from "../common/constants";
import LoadingOverlay from "react-loading-overlay";
import clientApi from "../api/clientApi";

function Gallery() {
  // define
  loadJs("/assets/js/custom.js");
  LoadingOverlay.propTypes = undefined;
  const [isLoading, setIsLoading] = useState(true);
  const [listHotelImage, setListHotelImage] = useState([]);

  // init
  useEffect(() => {
    fetchListHotelImage();
    window.scrollTo(0, 0);
  }, []);

  // function
  const fetchListHotelImage = async () => {
    const response = await clientApi.getAllGallery();
    if (response.responseCode === 200) {
      setListHotelImage(response.data);
      setIsLoading(false);
    }
  };
  return (
    <div>
      <LoadingOverlay active={isLoading} spinner text="Loading your content...">
        <section className="breadcrumb_area">
          <div
            className="overlay bg-parallax"
            data-stellar-ratio="0.8"
            data-stellar-vertical-offset="0"
            data-background=""
          ></div>
          <div className="container">
            <div className="page-cover text-center">
              <h2 className="page-cover-tittle">Bộ Sưu Tập</h2>
            </div>
          </div>
        </section>

        <section className="gallery_area section_gap">
          <div className="container">
            <div className="section_title text-center">
              <h2 className="title_color">Bộ Sưu Tập Ảnh</h2>
              <p>
                Những người cực kỳ yêu thích hệ thống thân thiện với môi trường.
              </p>
            </div>
            <div className="row imageGallery1" id="gallery">
              {listHotelImage.map((item) => (
                <div className="col-md-3 gallery_item" key={item.id}>
                  <div className="gallery_img">
                    <img src={item.imageLink} alt="" />
                  </div>
                </div>
              ))}
            </div>
          </div>
        </section>
      </LoadingOverlay>
    </div>
  );
}
export default Gallery;
