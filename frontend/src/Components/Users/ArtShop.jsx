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
    