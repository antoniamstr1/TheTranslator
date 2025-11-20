import "../style/Navbar.css";
import { useState, useEffect } from "react";
import axios from "axios";

interface NavbarProps {
  hasText: boolean;
  languageFrom: string;
  text: string;
  userCode: string;
  setUserCode: (value: string) => void;
  setText: (value: string) => void;
  setLanguage: (value: string) => void;
  setWords: (value: string[]) => void;
  textId: number | null;
  setTextId: (value: number | null) => void;


}

interface Text {
  id: number;
  userCode: string;
  content: string;
  languageFrom: string;
  languageTo: string;
  title: string;
}


export default function Navbar({ hasText, languageFrom, text, setText, userCode, setUserCode, setLanguage, setWords, textId, setTextId }: NavbarProps) {

  const storedCode = localStorage.getItem("user_code") || "";

  const [tempCode, setTempCode] = useState<string>(storedCode);

  const [isSaveInput, setIsSaveInput] = useState(false);
  const [textTitle, setTextTitle] = useState<string>("");
  const [titles, setTitles] = useState<Text[]>([]);

  // kada se promijeni text-id ili pri loadanju check da text-id postoji
  useEffect(() => {
    if (textId === null) return;

    const validate = async () => {
      try {
        const res = await axios.get(
          `${import.meta.env.VITE_API_BASE_URL}/texts/check/${textId}`
        );
        if (!res.data) {
          localStorage.removeItem("text-id");
        }

      } catch (error) {
        console.error("Failed to check code", error);
      }
    };

    validate();
  }, [textId]);

  useEffect(() => {
    if (!userCode) return;

    const fetchAndSet = async () => {
      try {
        const res = await axios.get<Text[]>(
          `${import.meta.env.VITE_API_BASE_URL}/texts/${userCode}`
        );
        setTitles(res.data);

        if (textId !== null) {
          const t = res.data.find(t => t.id === textId);
          if (t) {
            setText(t.content);
            setLanguage(t.languageFrom);
            setTextTitle(t.title);
            setWords([]);
          }
        }
      } catch (err) {
        console.error("Failed to fetch titles:", err);
      }
    };

    fetchAndSet();
  }, [userCode, textId]);



  const [carouselIndex, setCarouselIndex] = useState(0);

  type Direction = "left" | "right";


  const handleCarousel = (direction: Direction) => {
    let newIndex = carouselIndex;
    if (direction === "right") {
      newIndex = (carouselIndex + 1) % titles.length;
    } else if (direction === "left") {
      newIndex = (carouselIndex - 1 + titles.length) % titles.length;
    }
    setCarouselIndex(newIndex);
  };


  const changeCode = async (newCode: string) => {
    try {
      const response = await axios.get(
        `${import.meta.env.VITE_API_BASE_URL}/user/check-code/${newCode}`
      );
      if (response.data) {
        // zamjena kodova u localStorage
        localStorage.setItem("user_code", newCode);
        localStorage.removeItem("text-id");
        setUserCode(newCode);
        setText("");
        setLanguage("");
        setTextTitle("");
        setWords([]);
        setTitles([]);
      }
      if (!response.data) {
        setTempCode(userCode);
      }
    } catch (error) {
      console.error("Failed to check code", error);
    }
  };

  const handleSaveText = async () => {
    try {
      const res = await axios.post(`${import.meta.env.VITE_API_BASE_URL}/texts`, {
        userCode: userCode,
        content: text,
        languageFrom: languageFrom,
        languageTo: "eng",
        title: textTitle,
      });
      localStorage.setItem("text-id", res.data);

      await axios.get<Text[]>(
        `${import.meta.env.VITE_API_BASE_URL}/texts/${userCode}`
      );
      setTitles(res.data);
    } catch (err) {
      console.error("Save failed:", err);
      throw err;
    }
  };



  return (
    <div className="navbar">
      {/* right side */}
      <img className="navbar-element" src="title.png"></img>
      {/* left side */}
      <div className="left-side-navbar">
        {/* display saved texts */}
        <div className="navbar-texts">
          <img style={{ height: '1rem', width: 'auto', marginRight: "0.5rem" }} src="left-arrow.png" onClick={() => handleCarousel('left')} />
          <div className="title-carousel">
            {titles.map((t, i) => (
              <span
                key={t.id}
                id={`carousel-title-${t.id}`}
                className={i === carouselIndex ? "item" : ""}
                onClick={() => {
                  setTextId(t.id);
                  localStorage.setItem("text-id", t.id.toString());
                }}
              >{t.title}</span>
            ))}
          </div>
          <img style={{ height: '1rem', width: 'auto', marginLeft: "0.5rem" }} src="right-arrow.png" onClick={() => handleCarousel('left')} />
        </div>

        {/* save current workspace */}
        {isSaveInput && hasText ? (
          <input
            className="title-input"
            value={textTitle}
            onChange={(e) => setTextTitle(e.target.value)}
            autoFocus
            onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
              if (e.key === "Enter" || e.key === "Escape") {
                setIsSaveInput(false);
                handleSaveText();
              }
            }}
            onBlur={() => setIsSaveInput(false)}
          />
        ) : textTitle ? (
          <div className="title-div">
            <span className="title">{textTitle}</span>
            <img className="title-icons" src="plus.png"></img>
            <img className="title-icons" src="delete.png"></img>
          </div>
        ) : (
          <img
            className="save-icon"
            onClick={() => setIsSaveInput(true)}
            src={hasText ? "save.png" : "blocked-save.png"}
          />


        )}
        {/* logo -> copy code and create new workspace */}
        <div className="user-logo">

          <input
            className="workspace-input"
            type="text"
            value={tempCode}
            onChange={(e) => setTempCode(e.target.value)}
            onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
              if (e.key === "Enter") {
                changeCode((e.target as HTMLInputElement).value);
                // maknuti iz local.storage
                localStorage.removeItem("text_id");
              }
            }}
            maxLength={8}
            onBlur={() => setTempCode(userCode)}
          />
          <img className="user-icon" src="copy.png" />
        </div>

      </div>

    </div>
  );
}
