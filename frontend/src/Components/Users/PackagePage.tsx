import { Box, Card } from '@mui/material'
import React, { useContext, useEffect, useState } from 'react'
import "../../css/Package.css"
import { ThemeContext } from '../Themes/ThemeProvider.tsx';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, CardActions } from '@mui/material';
function DefaultCardStyle() {
    return (
        <Card className='cardDefault'>
            <CardActionArea>
                <CardMedia
                    component="img"
                    height="140"
                    image="/static/images/cards/contemplative-reptile.jpg"
                    alt="green iguana"
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        Lizard
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Lizards are a widespread group of squamate reptiles, with over 6,000
                        species, ranging across all continents except Antarctica
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Button size="small" color="primary">
                    Share
                </Button>
            </CardActions>
        </Card>
    )
}

function PremiumCardStyle() {
    return (
        <Card className='cardPremium'>
            <CardActionArea>
                <CardMedia
                    component="img"
                    height="140"
                    image="/static/images/cards/contemplative-reptile.jpg"
                    alt="green iguana"
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        Lizard
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Lizards are a widespread group of squamate reptiles, with over 6,000
                        species, ranging across all continents except Antarctica
                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions>
                <Button size="small" color="primary">
                    Share
                </Button>
            </CardActions>
        </Card>
    )
}

export default function PackagePage() {
    const { theme } = useContext(ThemeContext)
    return (
        <div className='packagePage'>
            <Box
                sx={{
                    color: theme.color,
                    backgroundColor: `rgba(${theme.rgbBackgroundColor},0.97)`,
                    transition: theme.transition,
                    width: '95%',
                    margin: 'auto',
                    borderRadius: '5px',
                    marginBottom: '15px',
                }}>
                <Box className="packageContainer">
                    <DefaultCardStyle />
                    <PremiumCardStyle />
                </Box>
            </Box>
        </div>
    )
}
