import React, { useEffect, useState } from "react";
import clientApi from "../api/clientApi";
import { Link } from "react-router-dom";
import LoadingOverlay from "react-loading-overlay";
import BannerHome from "./BannerHome";
import FooterHome from "./FooterHome";

function Home() {
  // define
  LoadingOverlay.propTypes = undefined;
  const [isLoading, setIsLoading] = useState(true);
  const [listProvinces, setListProvinces] = useState([]);

  // init
  useEffect(() => {
    fetchListProvinces();
    window.scrollTo(0, 0);
  }, []);

  //function
  const fetchListProvinces = async () => {
    const response = await clientApi.getAllProvinces();
    if (response.responseCode === 200) {
      setListProvinces(response.data);
      setIsLoading(false);
    }
  };

  return (
    <div>
      <LoadingOverlay active={isLoading} spinner text="Loading your content...">
        <BannerHome listProvinces={listProvinces}></BannerHome>

        <section className="blog_area single-post-area">
          <div className="container">
            <div className="section_title text-center">
              <h2 className="title_color">Điểm Đến Phổ Biến</h2>
            </div>
            <div className="row mb_30">
              {listProvinces.map((item) => (
                <div className="col-lg-3 col-md-6" key={item.id}>
                  <div className="accomodation_item text-center">
                    <div className="hotel_img">
                      <img src={item.imageLink} alt={item.name} />
                      <Link
                        to={`/hotel/${item.id}`}
                        className="btn theme_btn button_hover item-name"
                      >
                        <span>{item.name}</span>
                      </Link>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </section>

        <FooterHome></FooterHome>
      </LoadingOverlay>
    </div>
  );
}

export default Home;
