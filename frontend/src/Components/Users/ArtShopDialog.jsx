import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogTitle from '@mui/material/DialogTitle';
export default function ArtShopDialog(props) {
    let {
        open,
        handleYesClick,
        handleClose
    } = props;

    return (