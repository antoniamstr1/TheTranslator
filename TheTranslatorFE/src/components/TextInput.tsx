import "../style/TextInput.css";
import { useState } from 'react';
import type { MouseEvent } from "react";
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import Button from '@mui/material/ToggleButton';

interface TextProps {
    onTextClick: (words: string[]) => void;
}

export default function TextInput({ onTextClick }: TextProps) {
    const [text, setText] = useState("");

    const handleChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setText(e.target.value);
    };

    const handleButtonClick = () => {
        const words = Array.from(new Set(text.split(/\s+/).filter(Boolean)));
        onTextClick(words);
    };

    const [language, setLanguage] = useState<string | null>('it');

    const handleLanguage = (_event: MouseEvent<HTMLElement>, newLanguage: string | null) => {
        if (newLanguage !== null) {
            setLanguage(newLanguage);
        }
    };


    return (
        <div className="textInput" >
            <ToggleButtonGroup
                value={language}
                exclusive
                onChange={handleLanguage}
                aria-label="text alignment"
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
                placeholder="Type your text here..."
                value={text}
                onChange={handleChange}
            >
            </textarea>
            <Button
                className="translate-button"
                onClick={handleButtonClick}>Translate</Button>
        </div>

    )
}