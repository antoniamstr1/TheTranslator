import "../style/TextInput.css";
import { useState } from 'react';
import type { MouseEvent } from "react";
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';

interface TextProps {
    onTextClick: (words: string[]) => void;
    setLanguage: (language: string) => void;
    language: string;
}

export default function TextInput({ onTextClick, setLanguage, language }: TextProps) {
    const [text, setText] = useState("");

    const handleChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setText(e.target.value);
    };

    const handleButtonClick = () => {
        const words = Array.from(new Set(text
            .split(/[’\s]+/)
            .filter(Boolean)
            .map(word => word.toLowerCase()
                .replace(",", "")
                .replace(".", "")
                .replace("?", "")
                .replace("!", "")
                .replace(/"/g, "")
            )));
        onTextClick(words);

    };



    const handleLanguage = (_event: MouseEvent<HTMLElement>, newLanguage: string | null) => {
        if (newLanguage !== null) {
            setLanguage(newLanguage);
        }
    };

    return (
        <div className="textInput" >
            <ToggleButtonGroup
                exclusive
                onChange={handleLanguage}
                aria-label="text alignment"
                value={language}
            >
                <ToggleButton value="it" aria-label="it">
                    <p className="language-mark">it</p>
                </ToggleButton>
                <ToggleButton value="es" aria-label="sp">
                    <p className="language-mark">sp</p>
                </ToggleButton>

            </ToggleButtonGroup>
            <textarea
                className="translator-textarea"
                placeholder="What do you need help with?"
                value={text}
                onChange={handleChange}
            >
            </textarea>
            <button
                className="translate-button"
                onClick={handleButtonClick}>TRANSLATE</button>
        </div>

    )
}