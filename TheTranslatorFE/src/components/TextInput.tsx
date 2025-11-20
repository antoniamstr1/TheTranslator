import "../style/TextInput.css";
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import axios from "axios";


interface TextProps {
    onTextClick: (words: string[]) => void;
    setLanguage: (language: string) => void;
    language: string;
    text: string;
    setText: (value: string) => void;
    setHasText: (value: boolean) => void;
    textId: number | null;

}


export default function TextInput({ onTextClick, setLanguage, language, text, setText, setHasText, textId }: TextProps) {


    const handleTextChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setText(e.target.value);
        setHasText(e.target.value.trim().length > 0);
    };

    const handleButtonClick = async () => {
        const words = Array.from(
            new Set(
                text
                    .split(/[’'\s]+/)
                    .filter(Boolean)
                    .map(word =>
                        word
                            .toLowerCase()
                            .replace(",", "")
                            .replace(".", "")
                            .replace("?", "")
                            .replace("!", "")
                            .replace(/"/g, "")
                    )
            )
        );

        onTextClick(words);
        // ako se radi o spremljenom tekstu, onda updejtamo vrijednosti u bazi
        if (textId !== null) {
            try {
                await axios.put(
                    `${import.meta.env.VITE_API_BASE_URL}/texts/${textId}`,
                    {
                        content: text,
                        languageFrom: language,
                    }
                );
            } catch (err) {
                console.error("Failed to update text:", err);
            }
        }
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