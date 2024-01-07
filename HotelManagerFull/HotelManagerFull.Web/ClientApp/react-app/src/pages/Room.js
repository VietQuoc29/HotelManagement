import React, { useEffect, useState } from "react";
import LoadingOverlay from "react-loading-overlay";
import { Link, useParams } from "react-router-dom";
import clientApi from "../api/clientApi";
import { loadJs, STRING_EMPTY } from "../common/constants";
import NumberFormat from "react-number-format";
import Pagination from "@mui/material/Pagination";
import { FaStar, FaHotel, FaMapMarkerAlt, FaCheckCircle } from "react-icons/fa";
import $ from 'jquery';

function Room() {
    // define
    loadJs("/assets/js/custom.js");
    LoadingOverlay.propTypes = undefined;
    const [isLoading, setIsLoading] = useState(true);
    const [listRoomCategories, setListRoomCategories] = useState([]);
    const [listRoomStatus, setListRoomStatus] = useState([]);
    const [listRoom, setListRoom] = useState([]);
    const [hotelName, setHotelName] = useState(STRING_EMPTY);
    const [totalRecords, setTotalRecords] = useState(0);
    const { id } = useParams();
    const [roomStatusId, setRoomStatusId] = useState(STRING_EMPTY);
    const [roomCategoriesId, setRoomCategoriesId] = useState(STRING_EMPTY);
    const [star, setStar] = useState(STRING_EMPTY);
    const [page, setPage] = useState(1);
    const [pageSize] = useState(30);
    const [isIndexRoomStatus, setIsIndexRoomStatus] = useState(0);
    const [isIndexRoomCategories, setIsIndexRoomCategories] = useState(0);
    const [isStar, setIsStar] = useState(0);

    // init
    useEffect(() => {
        fetchDataRoomByHotel();
        if (isIndexRoomStatus === 0) {
            setTimeout(function () {
                $("#roomStatus_0").prop("checked", true);
            }, 380);
        }
        if (isIndexRoomCategories === 0) {
            setTimeout(function () {
                $("#roomCategories_0").prop("checked", true);
            }, 380);
        }
        if (isStar === 0) {
            setTimeout(function () {
                $("#star_0").prop("checked", true);
            }, 380);
        }
        window.scrollTo(0, 0);
    }, [page, roomStatusId, roomCategoriesId, star]);// eslint-disable-line react-hooks/exhaustive-deps

    // function
    const fetchDataRoomByHotel = async () => {
        const params = { hotelId: id, roomStatusId: roomStatusId, roomCategoriesId: roomCategoriesId, star: star, page: page, pageSize: pageSize };
        const response = await clientApi.getAllRoomByHotel(params);
        if (response.responseCode === 200) {
            setHotelName(response.data.hotelName);
            setListRoom(response.data.listRoom);
            setListRoomCategories(response.data.listRoomCategories);
            setListRoomStatus(response.data.listRoomStatus);
            setTotalRecords(Math.ceil(response.data.totalRecords / pageSize));
            setIsIndexRoomStatus(1);
            setIsIndexRoomCategories(1);
            setIsStar(1);
            setIsLoading(false);
        }
    };

    const handlePageChange = (event: ChangeEvent<unknown>, value: number) => {
        setPage(value);
    };

    const handleChangeRoomStatus = (event) => {
        setRoomStatusId(event.target.value);
    };

    const handleChangeRoomCategories = (event) => {
        setRoomCategoriesId(event.target.value);
    };

    const handleChangeStar = (event) => {
        setStar(event.target.value);
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
                            <h2>Danh Sách Phòng Khách Sạn {hotelName}</h2>
                            <p>
                                Phòng có đầy đủ tiện nghi, sạch sẽ, thoáng mát
                            </p>
                        </div>
                    </div>
                </section>

                <section className="blog_area single-post-area">
                    <div className="container">
                        <div className="row mb_30">
                            <div className="col-lg-3 col-md-3">
                                <div className="single-recent-blog-post">
                                    <div className="details">
                                        <h4 className="filter_by">Bộ lọc</h4>
                                        <legend><h4 className="uitk-heading-7">Trạng thái phòng</h4></legend>
                                        {listRoomStatus.map((item) => (
                                            <div key={item.id}>
                                                <input type="radio" id={'roomStatus_' + item.id} value={item.id} name="roomStatus" onChange={handleChangeRoomStatus} />
                                                <span>&nbsp;{item.name}</span>
                                            </div>
                                        ))}
                                        <br />
                                        <legend><h4 className="uitk-heading-7">Loại phòng</h4></legend>
                                        {listRoomCategories.map((item) => (
                                            <div key={item.id}>
                                                <input type="radio" id={'roomCategories_' + item.id} value={item.id} name="roomCategories" onChange={handleChangeRoomCategories} />
                                                <span>&nbsp;{item.name}</span>
                                            </div>
                                        ))}
                                        <br />
                                        <legend><h4 className="uitk-heading-7">Chất lượng phòng</h4></legend>
                                        <input type="radio" id="star_0" value="0" name="star" onChange={handleChangeStar} />
                                        <span>&nbsp;Tất cả</span>
                                        <br />
                                        <input type="radio" id="star_1" value="1" name="star" onChange={handleChangeStar} />
                                        <span>&nbsp;<FaStar /></span>
                                        <br />
                                        <input type="radio" id="star_2" value="2" name="star" onChange={handleChangeStar} />
                                        <span>&nbsp;<FaStar /><FaStar /></span>
                                        <br />
                                        <input type="radio" id="star_3" value="3" name="star" onChange={handleChangeStar} />
                                        <span>&nbsp;<FaStar /><FaStar /><FaStar /></span>
                                        <br />
                                        <input type="radio" id="star_4" value="4" name="star" onChange={handleChangeStar} />
                                        <span>&nbsp;<FaStar /><FaStar /><FaStar /><FaStar /></span>
                                        <br />
                                        <input type="radio" id="star_5" value="5" name="star" onChange={handleChangeStar} />
                                        <span>&nbsp;<FaStar /><FaStar /><FaStar /><FaStar /><FaStar /></span>
                                        <br />
                                    </div>
                                    <div className="thumb">
                                        <img className="img-fluid" src="https://lh5.googleusercontent.com/-zIr2ZTXltnM/U-ZIX7vr1UI/AAAAAAAADVk/Gwj-h_qhSls/s0/53e6485ed939c.jpg" alt="" />
                                    </div>
                                </div>
                            </div>
                            <div className="col-lg-9 col-md-9">
                                <div className="row">
                                    <ol className="no-bullet">
                                        {listRoom.map((item) => (
                                            <li className="result-li" key={item.id} >
                                                <div className="room-row">
                                                    <img className="uitk-image-media" src={item.image} alt="" />
                                                    <div className="info-room">
                                                        <Link to={`/room-detail/${item.id}`}><span className="uitk-heading-5">{item.name}
                                                            &nbsp;
                                                            {(() => {
                                                                switch (item.star) {
                                                                    case 1:
                                                                        return (
                                                                            <span>
                                                                                <FaStar />
                                                                            </span>
                                                                        )
                                                                    case 2:
                                                                        return (
                                                                            <span>
                                                                                <FaStar />
                                                                                <FaStar />
                                                                            </span>
                                                                        )
                                                                    case 3:
                                                                        return (
                                                                            <span>
                                                                                <FaStar />
                                                                                <FaStar />
                                                                                <FaStar />
                                                                            </span>
                                                                        )
                                                                    case 4:
                                                                        return (
                                                                            <span>
                                                                                <FaStar />
                                                                                <FaStar />
                                                                                <FaStar />
                                                                                <FaStar />
                                                                            </span>
                                                                        )
                                                                    case 5:
                                                                        return (
                                                                            <span>
                                                                                <FaStar />
                                                                                <FaStar />
                                                                                <FaStar />
                                                                                <FaStar />
                                                                                <FaStar />
                                                                            </span>
                                                                        )
                                                                    default:
                                                                        return (
                                                                            <span></span>
                                                                        )
                                                                }
                                                            })()}
                                                        </span>
                                                        </Link>
                                                        <br />
                                                        <span className="uitk-text-default-theme"><FaHotel /> {item.hotelName}</span>
                                                        <br />
                                                        <span className="uitk-text-default-theme"><FaMapMarkerAlt /> {item.floorName}</span>
                                                        <br />
                                                        <table className="table-room">
                                                            <tbody>
                                                                <tr>
                                                                    <td className="pwa-theme--success"><FaCheckCircle /> Đảm bảo hoàn tiền</td>
                                                                </tr>
                                                                <tr>
                                                                    <td className="pwa-theme--success"><FaCheckCircle /> Đảm bảo giá tốt nhất</td>
                                                                    <td className="uitk-price-origin">
                                                                        <NumberFormat
                                                                            value={item.price}
                                                                            displayType={"text"}
                                                                            thousandSeparator={true}
                                                                            prefix={""}
                                                                        /> vnđ</td>
                                                                </tr>
                                                                <tr>
                                                                    <td><span className="pwa-theme--status">{item.roomStatusName}</span></td>
                                                                    <td className="uitk-price-discount">
                                                                        <NumberFormat
                                                                            value={item.promotionalPrice}
                                                                            displayType={"text"}
                                                                            thousandSeparator={true}
                                                                            prefix={""}
                                                                        /> vnđ</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </li>
                                        ))}
                                    </ol>
                                </div>
                                <div className="row">
                                    <Pagination className="pagination-footer" count={totalRecords} page={page} onChange={handlePageChange} showFirstButton showLastButton />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

            </LoadingOverlay>
        </div >
    )
}
export default Room;