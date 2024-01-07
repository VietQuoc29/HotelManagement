import React, { useEffect, useState } from "react";
import LoadingOverlay from "react-loading-overlay";
import { Link, useParams } from "react-router-dom";
import clientApi from "../api/clientApi";
import { loadJs, STRING_EMPTY } from "../common/constants";
import { FaMapMarkedAlt } from "react-icons/fa";
import FooterHome from "./FooterHome";

function Provinces() {
  // define
  loadJs("/assets/js/custom.js");
  LoadingOverlay.propTypes = undefined;
  const [isLoading, setIsLoading] = useState(true);
  const [listHotel, setListHotel] = useState([]);
  const [provinceName, setProvinceName] = useState(STRING_EMPTY);
  const { id } = useParams();

  // init
  useEffect(() => {
    fetchData();
    window.scrollTo(0, 0);
  }, []);// eslint-disable-line react-hooks/exhaustive-deps

  // function
  const fetchData = async () => {
    const params = { provinceId: id };
    const response = await clientApi.getAllHotelByProvinces(params);
    if (response.responseCode === 200) {
      setProvinceName(response.data.provinceName);
      setListHotel(response.data.listHotel);
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
            <div className="page-cover text-center hotel-text">
              <h2>Khách Sạn Phổ Biến {provinceName}</h2>
              <p>
                Nhiều nơi lưu trú đã cập nhật với chúng tôi về các biện pháp sức
                khỏe và an toàn tăng cường
              </p>
            </div>
          </div>
        </section>

        <section className="blog_area single-post-area">
          <div className="container">
            <div className="row mb_30">
              {listHotel.map((item) => (
                <div className="col-lg-3 col-md-6" key={item.id}>
                  <div className="single-recent-blog-post">
                    <div className="thumb">
                      <img className="img-fluid" src={item.image} alt="" />
                    </div>
                    <div className="details">
                      <Link to={`/room/${item.id}`}>
                        <h4 className="sec_h4">{item.name}</h4>
                      </Link>
                      <h6 className="date title_color"><FaMapMarkedAlt /> {item.address}</h6>
                      <p>{item.title}</p>
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
export default Provinces;
