import { Backdrop, Box, Button, Card, CardActions, CardContent, CircularProgress, IconButton, Pagination, Stack, Typography } from '@mui/material'
import React, { useContext, useEffect, useState } from 'react'
import { useAuth } from '../AuthenContext.tsx'
import { getArtWithStatus } from '../../API/ArtShop/ArtShopServices.js';
import { Discount, Download, Headset, More, ShareLocation, Shop } from '@mui/icons-material';
import { pink } from '@mui/material/colors';
import "../../css/ArtShop.css";
import html2canvas from 'html2canvas';
import { Link } from 'react-router-dom';
import ArtShopDialog from './ArtShopDialog.jsx';
function ArtShop() {
    const auth = JSON.parse(sessionStorage.getItem("auth"));
    const [dataState, setDataSate] = useState([]);
    const [open, setOpen] = useState(false);
    const [openDowload, setOpenDowload] = useState(false);
    const handleClose = () => {
        setOpen(false);
        setOpenDowload(false);
        setDataSate((pre) => ({
            ...pre,
            idDowLoad: null
        }))
    };
    const handleOpen = () => {
        setOpen(true);
    };

    const search = async () => {
        try {
            handleOpen();
            const data = await getArtWithStatus(auth?.creatorID, dataState?.currentPage);
            setDataSate((pre) => ({
                ...pre,
                listItem: data?.data?.artworkViewModels,
                totalPages: data?.data?.totalPages,
            }))
        } catch (error) {

        } finally {
            handleClose();
        }
    }
    function formatMoney(amount) {
        return amount.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
    }

    const downloadSectionAsImage = async (elementId) => {
        const element = document.getElementById(elementId);

        if (element) {
            const canvas = await html2canvas(element);
            const imageUrl = canvas.toDataURL("image/png");
            const link = document.createElement("a");
            link.href = imageUrl;
            link.download = "image.png";
            link.click();
            handleClose();
        }
    };

    const handleYesClick = async () => {
        await downloadSectionAsImage(dataState?.idDowLoad);
    }

    const handleDownload = async (id) => {
        setDataSate((pre) => ({
            ...pre,
            idDowLoad: id
        }))
        setOpenDowload(true);
    };

    useEffect(() => {
        search();
    }, [])
    useEffect(() => {
        search();
    }, [dataState?.currentPage])