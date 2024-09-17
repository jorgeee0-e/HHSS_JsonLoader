<!-- Use preprocessors via the lang attribute! e.g. <template lang="pug"> -->
<template>
  <div id="app">
    <h2>Upload Files</h2>
    <div class="container">
      <div class="file-input-box">
        <div class="wrapper-file-input">
          <div class="input-box" @click="openFileInput">
            <h4>
              <i class="fa-solid fa-upload"></i>
              Choose File to upload
            </h4>
            <input
              ref="fileInput"
              type="file"
              hidden
              @change="handleFileChange"
              multiple
            />
          </div>
          <small>Files Supported: PDF, TEXT, DOC, DOCX, JPG, PNG, SVG</small>
        </div>

        <div class="wrapper-file-section">
          <div class="selected-files" v-if="selectedFileNames.length">
            <h5>Selected Files</h5>
            <ul
              class="file-list"
              :style="
                selectedFileNames.length ? 'max-height:220px' : 'height:auto'
              "
            >
              <transition-group name="fade" class="selected-files">
                <li
                  class="item"
                  v-for="(f, index) in selectedFileNames"
                  :key="f.name"
                >
                  <span class="name">
                    {{ f.name }} ({{ formatFileSize(f.size) }})
                  </span>
                  <div class="remove" @click="removeFile(index)">
                    <i class="fa-solid fa-trash-can"></i>
                  </div>
                </li>
              </transition-group>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      selectedFileNames: []
    };
  },
  methods: {
    openFileInput() {
      this.$refs.fileInput.click();
    },
    handleFileChange(event) {
      const fileList = event.target.files;
      const files = [];

      for (let i = 0; i < fileList.length; i++) {
        this.selectedFileNames.push({
          name: fileList[i].name,
          size: fileList[i].size
        });
      }
    },
    formatFileSize(size) {
      const units = ["B", "KB", "MB", "GB"];
      let index = 0;

      while (size >= 1024 && index < units.length - 1) {
        size /= 1024;
        index++;
      }

      return `${size.toFixed(2)} ${units[index]}`;
    },
    removeFile(index) {
      this.selectedFileNames.splice(index, 1);
    }
  }
};
</script>

<!-- Use preprocessors via the lang attribute! e.g. <style lang="scss"> -->
<style lang="scss">
@import url("https://fonts.googleapis.com/css2?family=DM+Mono:wght@300;400;500&display=swap");

$primary: #d90429;
$secondary: #1b2631;
$border: #8d99ae;

::-webkit-scrollbar {
  width: 4px;
  height: 6px;
}

::-webkit-scrollbar-track {
  -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.2);
  border-radius: 10px;
}

::-webkit-scrollbar-thumb {
  -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.4);
  background-color: #1b2631;
  border-radius: 10px;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease-in-out;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}

body {
  padding: 0;
  margin: 0;
  background-color: #17141d;
  color: $secondary;
  font-family: "DM Mono", monospace;
  width: 100%;
  height: 100vh;
  display: grid;
  place-items: center;
}
#app {
  h2 {
    color: white;
  }
  .file-input-box {
    display: flex;
    justify-content: center;
    flex-direction: column;
    border-radius: 10px;
    box-shadow: 0 5px 10px 0 rgba(0, 0, 0, 0.2);
    width: 600px;
    height: auto;
    background-color: #ffffff;
    padding: 20px;

    .input-box {
      padding: 20px;
      display: grid;
      place-items: center;
      border: 2px dashed #cacaca;
      border-radius: 5px;
      margin-bottom: 5px;
      cursor: pointer;
      h4 {
        margin: 0;
      }
    }
    small {
      font-size: 12px;
      color: #a3a3a3;
    }
    .wrapper-file-section {
      .selected-files {
        h5 {
          margin-bottom: 10px;
        }
        .file-list {
          overflow-y: auto;
          list-style-type: none;
          padding: 0 10px 10px 0;
          margin: 0;
          transition: 0.3s all ease-in-out;
          .item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border: 1px solid #cacaca;
            border-radius: 5px;
            padding: 10px;
            font-size: 14px;

            &:not(:last-child) {
              margin-bottom: 5px;
            }
            .remove {
              display: grid;
              place-items: center;
              color: #c0392b;
              cursor: pointer;
              transition: 0.3s all ease-in-out;
              &:hover {
                color: #e74c3c;
              }
            }
          }
        }
      }
    }
  }
}
</style>
