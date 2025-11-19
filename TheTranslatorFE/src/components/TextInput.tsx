import "../style/TextInput.css";
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';


interface TextProps {
    onTextClick: (words: string[]) => void;
    setLanguage: (language: string) => void;
    language: string;
    text: string;
    setText: (value: string) => void;
    setHasText: (value: boolean) => void;
}


export default function TextInput({ onTextClick, setLanguage, language, text, setText, setHasText}: TextProps) {





    const handleTextChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setText(e.target.value);
        setHasText(e.target.value.trim().length > 0);
    };

    const handleButtonClick = () => {
        const words = Array.from(new Set(text
            .split(/[’'\s]+/)
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


    return (
        <div className="textInput" >
            <ToggleButtonGroup
                exclusive
                onChange={(_event, newLanguage) => {
                    if (newLanguage !== null) {
                        setLanguage(newLanguage);
                    }
                }}
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
                onChange={handleTextChange}
            >
            </textarea>
            <button
                className="translate-button"
                onClick={handleButtonClick}>TRANSLATE</button>
        </div>

    )
}